SELECT s.*, p.*
FROM store AS s, person AS p
WHERE s.store_id = p.store_id