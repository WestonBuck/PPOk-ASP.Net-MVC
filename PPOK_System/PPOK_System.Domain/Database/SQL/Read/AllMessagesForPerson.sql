SELECT m.*, p1.*, p2.*, d.*
FROM message_history AS m, prescription AS p1, person AS p2, drug AS d
WHERE m.prescription_id = p1.prescription_id
	AND p1.person_id = p2.person_id
	AND p2.person_id = @param
	AND p1.drug_id = d.drug_id