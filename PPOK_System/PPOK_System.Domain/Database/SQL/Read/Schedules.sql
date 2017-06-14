SELECT s.*, p.person_id, pers.*
FROM scheduler AS s, prescription AS p, person AS pers
WHERE Convert(date, s.day_to_send) = Convert(date, GETDATE())
	AND s.prescription_id = p.prescription_id
	AND p.person_id = pers.person_id