<?php

// Get the raw POST data
$rawData = file_get_contents("php://input");
echo $rawData;

try {
    // Decode the JSON data
    $jsonData = json_decode($rawData, true);

    if ($jsonData === null) {
        throw new Exception("Error decoding JSON data.");
    }

    // Get the User-Agent from the HTTP headers
    $userAgent = isset($_SERVER['HTTP_USER_AGENT']) ? $_SERVER['HTTP_USER_AGENT'] : '';

    // Include User-Agent in the $jsonData array
    $jsonData['userAgent'] = $userAgent;

    // Construct the request URL for ip-api.com
    $ipApiUrl = 'http://ip-api.com/json/' . $jsonData['ip'] . '?fields=message,country,countryCode,city,zip,lat,lon,timezone,isp,org,mobile,proxy,query';

    // Make the request to ip-api.com
    $ipApiResponse = file_get_contents($ipApiUrl);

    if ($ipApiResponse === false) {
        throw new Exception("Error making request to ip-api.com.");
    }

    // Decode the response from ip-api.com
    $ipApiData = json_decode($ipApiResponse, true);

    if ($ipApiData === null) {
        throw new Exception("Error decoding response from ip-api.com.");
    }

    // Combine the data from ipify, ip-api, and User-Agent
    $combinedData = array_merge($jsonData, $ipApiData);

    // Specify the filename
    $filename = 'user_data.json';

    // Load existing data from the JSON file (if it exists)
    $existingData = [];

    if (file_exists($filename)) {
        $existingData = json_decode(file_get_contents($filename), true);

        if ($existingData === null) {
            throw new Exception("Error decoding existing data from JSON file.");
        }

        // Check if the IP already exists in the data
        $existingIpAddresses = array_column($existingData, 'ip');

        if (in_array($jsonData['ip'], $existingIpAddresses)) {
            throw new Exception("IP address already exists in the JSON file.");
        }
    }

    // Generate a unique key for the new user entry
    $uniqueKey = 'uniquekey' . uniqid();

    // Append the new data to the existing data array with the unique key
    $existingData[$uniqueKey] = $combinedData;

    // Save the combined data to the JSON file
    file_put_contents($filename, json_encode($existingData, JSON_PRETTY_PRINT));

    // Send a success response
    echo 'Data saved successfully to ' . $filename;
} catch (Exception $e) {
    // Handle exceptions
    echo 'Error: ' . $e->getMessage();
}
?>
