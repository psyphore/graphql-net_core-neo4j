WITH '{"FirstName":"Sipho","LastName":"Hlophe","DateOfBirth":"1987-03-09T00:00:00+02:00","MobileNumber":"0719999999","EmailAddress":"sipho.hlophe@continetal.hightable.org","Address":{"Line1":"2 Carlswald Meadows","Line2":"55 Acacia Road","Suburb":"Midrand","Zip":"1685","Country":"South Africa","ContactNumbers":{"$id":"2","$values":["0719999999"]}}}' AS l
WITH apoc.json.path(l) AS lead
, apoc.json.path(l, '$.Address') AS address
, apoc.json.path(l, '$.Address.ContactNumbers..$values') AS contacts
, timestamp() AS createdOn
CALL {
    WITH lead, createdOn
    MERGE (l:Lead{ id: apoc.create.uuid(), created: createdOn })
    ON CREATE SET l += lead { 
        .FirstName,
        .LastName,
        .DateOfBirth,
        .MobileNumber,
        .EmailAddress
    }
    RETURN l
}
CALL {
    WITH address, createdOn
    CREATE (a:Address {created: createdOn})
    SET a += address {
        .Line1,
        .Line2,
        .Line3,
        .Suburb,
        .Zip,
        .Country
    }
    RETURN a
}
CALL {
    WITH contacts, createdOn
    UNWIND contacts AS contact
    CREATE (c:Contact { id: apoc.create.uuid(), created: createdOn, number: contact})
    RETURN c
}
CALL {
    WITH l,a,c, createdOn
    CREATE (l)-[r1:RESIDES_AT{created: createdOn}]->(a)<-[r2:HAS_CONTACT]-(c)
    RETURN r1,r2
}
RETURN l, r1,r2, a, c