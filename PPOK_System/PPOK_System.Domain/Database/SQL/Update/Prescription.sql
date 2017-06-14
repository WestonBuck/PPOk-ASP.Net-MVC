UPDATE prescription
SET person_id = @person_id, drug_id = @drug_id, date_filled = @date_filled,
	days_supply = @days_supply, number_refills = @number_refills
WHERE prescription_id = @prescription_id