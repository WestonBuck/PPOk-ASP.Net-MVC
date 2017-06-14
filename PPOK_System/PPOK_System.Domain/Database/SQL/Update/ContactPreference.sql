UPDATE contact_preference
SET person_id = @person_id, contact_type = @contact_type
WHERE preference_id = @preference_id