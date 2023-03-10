WITH { currency:'ZAR', price: 80.0} AS val
MERGE (P2:Product { id: apoc.create.uuid(), name:'Gigabyte GA-X670 AOSUS-ELITE-AX', tags: ['motherboard', 'AMD', 'AM5']})
SET P2.created = timestamp(), 
    P2.price.amount = val { .price },
    P2.price.currency = val { .currency }
    
RETURN P2;