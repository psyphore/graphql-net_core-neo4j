using System.Collections.Generic;

namespace DataAccess.Building
{
    public class BuildingQueries
    {
        public IDictionary<string, string> Mutations =>
            new Dictionary<string, string>
            {
                { "UPDATE_BUILDING", @"
                        MERGE (b:Building{id: $id})
                        SET b += {
                          name: $name, 
                          address: $address
                        }
                        RETURN b AS building;
                    "},
                { "UPDATE_BUILDING_RESIDENCY", @"
                        MATCH (p:Person{id:$personId}-[r:BASED_IN]->(b:Building{id:$buildingId}))
                        MERGE (p)-[r2:BASED_IN]->(b)
                        WITH r, b, p, r2
                        DELETE r
                        RETURN b AS building
                    "},
                { "DEACTIVATE_BUILDING", @"
                        MATCH (b:Building) 
                        WHERE b.id = $id
                        SET b.deactivated = timestamp()
                        RETURN b AS building
                    " }
            };

        public IDictionary<string, string> Queries =>
            new Dictionary<string, string>
            {
                { "GET_BUILDING", @"
                        MATCH (b:Building)
                        WHERE LOWER(b.name) CONTAINS LOWER({name}) OR b.id = $id
                        RETURN b { 
                          .id , 
                          .name , 
                          .address ,
                          headcount: apoc.cypher.runFirstColumn(""RETURN SIZE((this)<-[:BASED_IN]-())"", {this: b}, false),
                          people: [(b)<-[:BASED_IN]-(p) | p] }
                        AS building
                    "},
                { "GET_BUILDINGS", @"
                       MATCH (building:Building)
                        RETURN building { 
                            .id , 
                            .name , 
                            .address ,
                            headcount: apoc.cypher.runFirstColumn(""RETURN SIZE((this)<-[:BASED_IN]-())"", {this: building}, false),
                            people: [(building)<-[:BASED_IN]-(p) | p] }
                        AS building
                        SKIP $offset
                        LIMIT $first
                    " }
            };
    }
}