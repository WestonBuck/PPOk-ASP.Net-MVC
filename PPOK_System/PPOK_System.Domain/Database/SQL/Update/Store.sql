UPDATE store
SET [name] = @name, [address] = @address, [city] = @city, [state] = @state, [zip] = @zip
WHERE store_id = @store_id