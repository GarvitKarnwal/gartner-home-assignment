/*A:1*/ 
SELECT * 
FROM   users u 
WHERE  u.id IN ( 2, 3, 4 ) 
ORDER  BY u.first_name, 
          u.last_name 





/*A:2*/ 
SELECT u.first_name, 
       u.last_name, 
       Sum(CASE 
             WHEN l.status = 2 THEN 1 
             ELSE 0 
           END) AS 'basic', 
       Sum(CASE 
             WHEN l.status = 3 THEN 1 
             ELSE 0 
           END) AS 'premium' 
FROM   users u 
       LEFT JOIN listings l 
              ON l.user_id = u.id 
WHERE  u.status = 2 
GROUP  BY u.id, 
          u.first_name, 
          u.last_name 
ORDER  BY u.first_name, 
          u.last_name 





/*A:3*/ 
SELECT u.first_name, 
       u.last_name, 
       Sum(CASE 
             WHEN l.status = 2 THEN 1 
             ELSE 0 
           END) AS 'basic', 
       Sum(CASE 
             WHEN l.status = 3 THEN 1 
             ELSE 0 
           END) AS 'premium' 
FROM   users u 
       LEFT JOIN listings l 
              ON l.user_id = u.id 
WHERE  u.status = 2 
GROUP  BY u.id, 
          u.first_name, 
          u.last_name 
HAVING Max(l.status) = 3 
ORDER  BY u.first_name, 
          u.last_name 




/*A:4*/ 
SELECT u.first_name, 
       u.last_name, 
       Isnull(c.currency, '')  AS 'currency', 
       Sum(Isnull(c.price, 0)) AS 'revenue' 
FROM   clicks c--top 4 
       INNER JOIN listings l 
               ON c.listing_id = l.id 
                  AND Year(c.created) = 2013 
       RIGHT JOIN users u 
               ON u.id = l.user_id 
WHERE  u.status = 2 
GROUP  BY u.id, 
          u.first_name, 
          u.last_name, 
          c.currency 
ORDER  BY u.first_name, 
          u.last_name 




/*A:5*/ 
INSERT INTO [dbo].[clicks] 
            ([listing_id], 
             [price], 
             [created]) 
VALUES      (3, 
             4, 
             Getdate()) 

SELECT Scope_identity() AS 'id' 





/*A:6*/ 
SELECT l.NAME 
FROM   listings l 
       LEFT JOIN clicks c 
              ON c.listing_id = l.id 
                 AND Year(c.created) = 2013 
WHERE  c.id IS NULL 
ORDER  BY l.NAME 





/*A:7*/ 
SELECT Year(c.created)        AS date, 
       Count(DISTINCT l.NAME) AS 'total_listings_clicked', 
       Count(DISTINCT u.id)   AS 'total_vendors_affected' 
FROM   clicks c 
       INNER JOIN listings l 
               ON c.listing_id = l.id 
       INNER JOIN users u 
               ON u.id = l.user_id 
GROUP  BY Year(c.created) 
ORDER  BY Year(c.created)--,l.name 





/*A:8*/ 
SELECT u.first_name, 
       u.last_name, 
       listing_names = Stuff((SELECT ', ' + l.NAME 
                              FROM   listings l 
                              WHERE  l.user_id = u.id 
                              ORDER  BY l.NAME 
                              FOR xml path(''), type).value('.', 'VARCHAR(MAX)') 
                       , 1, 1, 
                       Space(0)) 
FROM   users u 
WHERE  u.status = 2 
GROUP  BY u.id, 
          u.first_name, 
          u.last_name 
ORDER  BY u.first_name, 
          u.last_name 