UPDATE person
SET person_id = @person_id, store_id = @store_id, first_name = @first_name,
	last_name = @last_name, zip = @zip, email = @email, password = @password,
	phone = @phone, date_of_birth = @date_of_birth, person_type = @person_type
WHERE person_id = @person_id