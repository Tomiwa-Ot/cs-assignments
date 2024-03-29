<?php

define('HOST', 'localhost');
define('USERNAME', 'root');
define('PASSWORD', 'root');
define('DATABASE', 'Science');

$connection = mysqli_connect(HOST, USERNAME, PASSWORD, DATABASE);

if (!$connection)
    exit(1);
