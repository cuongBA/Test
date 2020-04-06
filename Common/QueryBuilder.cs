using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
namespace Test.Common
{
    public class QueryBuilder
    {
        public static SpanFirstQuery match_first_word(string field, string value, double boost = 0)
        {
            if(boost>0)
            {
                return new SpanFirstQuery
                {
                    Boost = boost,
                    Match = new SpanQuery
                    {
                        SpanTerm = new SpanTermQuery
                        {
                            Field = field,
                            Value = value.ToLower()
                        }
                    },
                    End = 1
                };
            }
            else
            {
                return new SpanFirstQuery
                {
                    Match = new SpanQuery
                    {
                        SpanTerm = new SpanTermQuery
                        {
                            Field = field,
                            Value = value.ToLower()
                        }
                    },
                    End = 1
                };
            }
        }

        public static MatchPhrasePrefixQuery match_phrase_prefix(string field, string value)
        {
            return new MatchPhrasePrefixQuery
            {
                Field = field,
                Query = value.ToLower()
            };
        }

        public static MatchQuery match_phrase(string field, string value, bool fuzzy = false)
        {
            string _value = value.ToLower() ?? String.Empty;
            int len = _value.Length;
            IFuzziness fuzz;
            switch(len)
            {
                case int n when len <= 2: fuzz = Fuzziness.EditDistance(1); break;
                case int n when len == 3: fuzz = Fuzziness.EditDistance(2); break;
                default: fuzz = Fuzziness.Auto; break;
            }

            var query = new MatchQuery
            {
                Field = field,
                Query = _value,
                Operator = Operator.And
            };
            if (fuzzy) query.Fuzziness = fuzz;
            return query;

        }

        public static SpanNearQuery span_query(string field, List<string> tokens)
        {
            List<ISpanQuery> list = new List<ISpanQuery>();
            foreach(var x in tokens)
            {
                var subQuery = new SpanQuery();
                subQuery.SpanTerm = new SpanTermQuery { Field = field, Value = x.ToLower() };
                list.Add(subQuery);
            }
            return new SpanNearQuery
            {
                Clauses = list,
                Slop = 100,
                InOrder = true
            };
        }

    }
}
