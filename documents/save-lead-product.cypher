WITH '{"$id":"1","FirstName":"Viggo","LastName":"Tarasov","DateOfBirth":"1956-03-01T00:00:00+02:00","MobileNumber":"0718890002","EmailAddress":"vigo.tarasov@continetal.hightable.org","Address":{"$id":"2","Line1":"6 Carlswald Meadows","Line2":"55 Acacia Road","Suburb":"Midrand","Zip":"1685","Country":"South Africa","ContactNumbers":{"$id":"3","$values":["0718890002"]}},"Id":"a2f4dd81-7e7d-4a4e-9469-9820e1714da4"}' AS l
WITH apoc.json.path(l) AS lead
, apoc.json.path(l, '$.Address') AS address
, apoc.json.path(l, '$.Address.ContactNumbers..$values') AS contacts
, timestamp() AS createdOn
CALL {
    WITH lead, createdOn
    MERGE (l:Lead{ Id: apoc.create.uuid(), Created: createdOn, Active: false })
    ON CREATE SET l += lead { 
        .FirstName,
        .LastName,
        .DateOfBirth,
        .EmailAddress
    }
    RETURN l
}
CALL {
    WITH address, createdOn
    CREATE (a:Address {Created: createdOn})
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
    CREATE (c:Contact { Cd: apoc.create.uuid(), Created: createdOn, Number: contact })
    RETURN c
}
CALL {
    WITH l,a,c, createdOn
    CREATE (a)<-[r1:RESIDES_AT{ Created: createdOn }]-(l)<-[r2:HAS_CONTACT {Created: createdOn}]-(c)
    RETURN r1,r2
}
RETURN l.Id