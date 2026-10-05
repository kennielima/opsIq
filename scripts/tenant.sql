-- hash key
echo -n "sk_newtenantkey" | shasum -a 256 | awk '{print $1}' 


INSERT INTO "Tenants" ("Id", "Name", "Slug", "CreatedAt")                                                             
  VALUES (gen_random_uuid(), :tenant_name, :tenant_slug, NOW());                                                        
                                                                                                                        
INSERT INTO "ApiKeys" ("Id", "TenantId", "HashedKey", "CreatedAt")                                                    
  VALUES (                                                                                                              
    gen_random_uuid(),                                                                                                  
    (SELECT "Id" FROM "Tenants" WHERE "Slug" = :tenant_slug),                                                           
    :api_key_hash,                                                                                                      
    NOW()                                                                                                               
  );                                                                                                                    


-- demo
INSERT INTO "Tenants" ("Id", "Name", "Slug", "CreatedAt")                                                              
  VALUES (gen_random_uuid(), 'AcmePay', 'acmepay', NOW());

INSERT INTO "ApiKeys" ("Id", "TenantId", "HashedKey", "CreatedAt")                                                     
  VALUES (                                                                                                               
      gen_random_uuid(),                                                                                                 
      (SELECT "Id" FROM "Tenants" WHERE "Slug" = 'acmepay'),                                                             
      '50b426454d95651fbcf720c0660d4e90ae101be9285f50a23053ad148eeefb2b',                                                
      NOW()
    );