WITH { currency:'ZAR', price: 80.0} AS val
MERGE (P2:Product { id: apoc.create.uuid(), name:'Gigabyte GA-X670 AOSUS-ELITE-AX', tags: ['motherboard', 'AMD', 'AM5']})
SET P2.created = timestamp(), 
    P2.price.amount = val { .price },
    P2.price.currency = val { .currency }
    
RETURN P2;

// get a single lead
OPTIONAL MATCH (l:Lead{Id: "7828d582-3aa1-4e19-81cc-548e2919b102"})
CALL {
    WITH l
    OPTIONAL MATCH (l)--(c:Contact)
    UNWIND c.Number AS numbers
    RETURN numbers
}
CALL{
    WITH l
    OPTIONAL MATCH (l)--(a:Address)
    //UNWIND a AS addresses
    RETURN a AS addresses
}
RETURN l {
    id: l.Id,
    firstName: l.FirstName,
    lastName: l.LastName,
    dateOfBirth: l.DateOfBirth,
    emailAddress: l.EmailAddress,
    address: addresses { .* },
    numbers: numbers
} AS Lead

```json
{
  "firstName": "Viggo",
  "lastName": "Tarasov",
  "emailAddress": "vigo.tarasov@continetal.hightable.org",
  "address": {
    "Zip": "1685",
    "Suburb": "Midrand",
    "Country": "South Africa",
    "Line1": "6 Carlswald Meadows",
    "Line2": "55 Acacia Road",
    "Created": 1678773644627
  },
  "numbers": "0718890002",
  "dateOfBirth": "1956-03-01T00:00:00+02:00",
  "id": "7828d582-3aa1-4e19-81cc-548e2919b102"
}
```