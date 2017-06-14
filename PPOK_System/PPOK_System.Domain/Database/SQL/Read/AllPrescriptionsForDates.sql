SELECT p1.*, p2.*, d.*
FROM prescription AS p1, person AS p2, drug AS d
WHERE p1.person_id = p2.person_id
	AND p1.drug_id = d.drug_id
	AND d.drug_id = @param
	AND date_filled >= @param2
	AND date_filled <= @param3