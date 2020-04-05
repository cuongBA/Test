using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.DataAccess;
using Test.Common;
using Microsoft.Data.SqlClient;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest;
using System.Data;
using System.Diagnostics;
namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DmlController : ControllerBase
    {
        private readonly ElasticClient client = Global.CreateEsClient();
        private readonly string _index_name = "location";
        private readonly string _type_name = "record";

        // api: import
        [HttpPost("import")]
        public async Task<IActionResult> Import()
        {

            //check xem index đã tồn tại chưa, nếu chưa có thì xóa đi
            if (client.Indices.Exists(_index_name).Exists)
            {
                client.Indices.Delete(_index_name);
            }

            string ConnectionString = Global.ConnectionString();
            LocationDataAccess dal = new LocationDataAccess(ConnectionString);
            SqlConnection con = dal.connect;
            
            var operation = new { index = new { _index = _index_name, _type = _type_name } }; 
            //object này không được phép sửa, nếu không bulk sẽ fail

            string commandText = @"select * from [dbo].[DTS.PointOfInterest]";

            string bulkString = dal.GetBulkStringForImport(con, commandText, operation);
            //tạo string phục vụ bulk insert


            var bulkResponse = client.LowLevel.Bulk<BulkResponse> (bulkString);
            if (bulkResponse.Errors)
            {
                client.Indices.Delete(_index_name);
                return BadRequest();
            }
            return Ok(bulkResponse);

        }

        [HttpPost("importhighlevel")]
        public async Task<IActionResult> ImportHighLevel()
        {
            //check xem index đã tồn tại chưa, nếu chưa có thì xóa đi
            if (client.Indices.Exists(_index_name).Exists)
            {
                client.Indices.Delete(_index_name);
            }

            string ConnectionString = Global.ConnectionString();
            LocationDataAccess dal = new LocationDataAccess(ConnectionString);
            SqlConnection con = dal.connect;

            var operation = new { index = new { _index = _index_name, _type = _type_name } };
            //object này không được phép sửa, nếu không bulk sẽ fail

            string commandText = @"select * from [dbo].[DTS.PointOfInterest]";

            BulkRequest bulkReq = dal.GetBulkRequest(con, commandText, operation);



            var bulkResponse = client.Bulk(bulkReq);
            if (bulkResponse.Errors)
            {
                client.Indices.Delete(_index_name);
                return BadRequest();
            }
            return Ok(bulkResponse);
        }      

        [HttpPost("replace")]
        public async Task<IActionResult> ReplaceDocument([FromForm] string Id)
        {
            if(String.IsNullOrEmpty(Id)) return NotFound();
            var getRequest = new GetRequest(_index_name, Id);
            var getResponse = client.Get<dynamic>(getRequest);
            if(!getResponse.Found) return NotFound();

            var indexRequest = new IndexRequest<dynamic>(_index_name, Id);
            indexRequest.Document = new { street = "K1" };

            client.Index(indexRequest);
            return Ok();
        }

        [HttpGet("updatefield")]
        public async Task<IActionResult> UpdateField([FromQuery]string Id, string fieldName, string newValue)
        {
            if (String.IsNullOrEmpty(Id)) return NotFound();
            var getRequest = new GetRequest(_index_name, Id);
            var getResponse = client.Get<dynamic>(getRequest);
            if (!getResponse.Found) return NotFound();

            var doc = getResponse.Source as Dictionary<string, object>;

            if(!doc.ContainsKey(fieldName)) return NotFound( new { message = "Property not exists" });
            var docPath = new DocumentPath<dynamic>(Id);
            var response = client.Update<dynamic>(docPath, u => u
                    .Index(_index_name)
                    .Script(script => script
                    .Source(string.Format(@"ctx._source.{0} = params.newValue", fieldName))
                    .Params(p => p
                            .Add("newValue", newValue)
                            ))
                    .Refresh(Refresh.True));
            return Ok(response);
        }

        [HttpGet("addfield")]
        public async Task<IActionResult> AddField([FromQuery]string Id, string fieldName, string newValue)
        {
            if (String.IsNullOrEmpty(Id)) return NotFound();
            var getRequest = new GetRequest(_index_name, Id);
            var getResponse = client.Get<dynamic>(getRequest);
            if (!getResponse.Found) return NotFound();
            

            var docPath = new DocumentPath<dynamic>(Id);
            var response = client.Update<dynamic>(docPath, u => u
                    .Index(_index_name)
                    .Script(script => script
                    .Source(string.Format(@"ctx._source.{0} = params.newValue", fieldName))
                    .Params(p => p
                            .Add("newValue", newValue)
                            ))
                    .Refresh(Refresh.True));
            return Ok(response);
        }

        [HttpGet("removefield")]
        public async Task<IActionResult> RemoveField([FromQuery]string Id, string fieldName)
        {
            if (String.IsNullOrEmpty(Id)) return NotFound();
            var getRequest = new GetRequest(_index_name, Id);
            var getResponse = client.Get<dynamic>(getRequest);
            if (!getResponse.Found) return NotFound();

            var doc = getResponse.Source as Dictionary<string, object>;
            if (!doc.ContainsKey(fieldName)) return NotFound(new { message = "Property not exists" });

            var docPath = new DocumentPath<dynamic>(Id);
            var response = client.Update<dynamic>(docPath, u => u
                    .Index(_index_name)
                    .Script(script => script
                    .Source(string.Format(@"ctx._source.remove('{0}')", fieldName)))
                    .Refresh(Refresh.True));
            return Ok(response);
        }

        [HttpPost("removedoc")]
        public async Task<IActionResult> RemoveDoc([FromForm]string Id)
        {
            if (String.IsNullOrEmpty(Id)) return NotFound();
            var getRequest = new GetRequest(_index_name, Id);
            var getResponse = client.Get<dynamic>(getRequest);
            if (!getResponse.Found) return NotFound();


            var docPath = new DocumentPath<dynamic>(Id).Index(_index_name);
            var response = client.Delete<dynamic>(docPath, d => d.Refresh(Refresh.True));
            return Ok();
        }
    }
}
