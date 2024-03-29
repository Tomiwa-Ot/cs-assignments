<?php

/**
 * Generate STAFF table query using GET
 * parameters as filters
 */
function generateQuery() {
    $filters = array();

    // Filter by month
    if (isset($_GET['month']))
        $filters[] = "MONTH(date_of_birth) = " . $_GET['month'];

    // Filter by staff type
    if (isset($_GET['staff_type']))
        $filters[] = "staff_type = '" . $_GET['staff_type'] . "'";

    // Filter by department
    if (isset($_GET['department']))
        $filters[] = "department = " . $_GET['department'];

    // Filter by gender
    if (isset($_GET['gender']))
        $filters[] = "gender = '" . $_GET['gender'] . "'";

    // Construct sql query filters
    $queryConstraint = "";
    if (!empty($filters))
        $queryConstraint = " WHERE ". implode(" AND ", $filters);

    // Add limit to query results
    $page = 1;
    $resultsPerPage = 5;
    if (isset($_GET['page']))
        $page = $_GET['page'];

    $startFrom = ($page - 1) * $resultsPerPage;
    $queryLimit = " LIMIT $startFrom, $resultsPerPage";

    $query = "SELECT * FROM STAFF" . $queryConstraint . $queryLimit;

    return $query;
}

/**
 * Add staff records to database
 */
function registerStaff($connection) {
    if (!isset($_POST['lastname']) || empty($_POST['lastname']))
        return false;

    if (!isset($_POST['firstname']) || empty($_POST['firstname']))
        return false;

    if (!isset($_POST['dob']) || empty($_POST['dob']))
        return false;

    if (!isset($_POST['gender']) || empty($_POST['gender']))
        return false;

    if (!isset($_POST['department']) || empty($_POST['department']))
        return false;

    if (!isset($_POST['staff_type']) || empty($_POST['staff_type']))
        return false;

    // Filter user input
    $lastname = mysqli_real_escape_string($connection, $_POST['lastname']);
    $firstname = mysqli_real_escape_string($connection, $_POST['firstname']);
    $dob = mysqli_real_escape_string($connection, $_POST['dob']);
    $gender = mysqli_real_escape_string($connection, $_POST['gender']);
    $department = mysqli_real_escape_string($connection, $_POST['department']);
    $staffType = mysqli_real_escape_string($connection, $_POST['staff_type']);

    // Insert into database
    $query = "
        INSERT INTO STAFF(lastname, firstname, date_of_birth, gender, department, staff_type) 
        VALUES('$lastname', '$firstname', '$dob', '$gender', $department, '$staffType')
    ";
    $output = mysqli_query($connection, $query);
    
    return $output;
}

/**
 * Get all departments in the database
 */
function getDepartments($connection) {
    $query = "SELECT * FROM DEPARTMENT";
    $output = mysqli_query($connection, $query);
    $departments = mysqli_fetch_all($output, MYSQLI_ASSOC);
    
    return $departments;
}

/**
 * Get department name by id
 */
function getDepartment($connection, $id) 
{
    // Filter data
    $id = mysqli_real_escape_string($connection, $id);

    // Build and execute query
    $query = "SELECT name FROM DEPARTMENT WHERE id=$id";
    $output = mysqli_query($connection, $query);

    if ($output) {
        $department = mysqli_fetch_assoc($output);
        echo $department['name'];
    }
}

/**
 * Set chosen filter as the dropdown placeholder
 */
function isFilterSelected($key, $value) {
    // Check if the GET variable is set
    if (isset($_GET[$key]) && $_GET[$key] == $value)
        echo "selected";
}