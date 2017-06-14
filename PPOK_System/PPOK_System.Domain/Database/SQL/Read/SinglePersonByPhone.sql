SELECT p.*, s.*, c.*
FROM person AS p, store AS s, contact_preference AS c
WHERE p.person_id = c.person_id
	AND s.store_id = p.store_id
	AND p.phone = @param