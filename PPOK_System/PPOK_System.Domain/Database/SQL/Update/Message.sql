UPDATE message_history
SET prescription_id = @prescription_id, response = @response,
	fill_time = @fill_time, pick_up_time = @pick_up_time
WHERE message_id = @message_id