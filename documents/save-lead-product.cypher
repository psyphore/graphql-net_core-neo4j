WITH '{"$id":"1","FirstName":"Johnathan","LastName":"Wick","DateOfBirth":"1976-03-01T00:00:00+02:00","Contacts":[{"$id":"2","Number":"0718890001","Email":"john.wick@continetal.hightable.org"}],"Addresses":[{"$id":"3","Line1":"54 Carlswald Meadows","Line2":"55 Acacia Road","Suburb":"Midrand","Zip":"1685","Country":"South Africa"}],"Id":""}' AS l
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