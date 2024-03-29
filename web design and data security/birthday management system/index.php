<?php

require __DIR__ . "/database.php";
require __DIR__ . "/utils.php";

// Register staff if request is POST
$registrationStatus = null;
if (isset($_POST['submit'])) {
    $status = registerStaff($connection);
    if ($status)
        $registrationStatus = "Registration Successful";
    else
        $registrationStatus = "Registration Failed";
}

// Fetch staff data
$query = generateQuery();
$output = mysqli_query($connection, $query);
$staff_members = mysqli_fetch_all($output, MYSQLI_ASSOC);

require __DIR__ . "/header.php";
?>

<div class="container">
    <h1>Birthday Management System</h1>

<div class="top-cont">
    <!-- Toggle Modal Icon -->
    <div>
        <?php if (isset($registrationStatus)): ?>
            <h3><?= $registrationStatus ?></h3>
        <?php endif ?>
        <button id="open-modal">&plus;</button>
    </div>

    <!-- Filter Section -->
    <div class="filters">
        <!-- Gender filter -->
        <label>Gender: </label>
        <select name="gender" class="filter" id="gender">
            <option value=""></option>
            <option value="Male" <?= isFilterSelected('gender', 'Male') ?>>Male</option>
            <option value="Female" <?= isFilterSelected('gender', 'Female') ?>>Female</option>
        </select>

        <!-- Department filter -->
        <label>Department: </label>
        <select name="department" class="filter" id="department">
            <option value=""></option>
            <?php foreach(getDepartments($connection) as $department): ?>
                <option value="<?= $department['id'] ?>" <?= isFilterSelected('department', $department['id']) ?>>
                    <?= $department['name'] ?>
                </option>
            <?php endforeach ?>
        </select>

        <!-- Staff type filter -->
        <label>Staff Type: </label>
        <select name="staff_type" class="filter" id="staff_type">
            <option value=""></option>
            <option value="Academic" <?= isFilterSelected('staff_type', 'Academic') ?>>Academic</option>
            <option value="Non-academic" <?= isFilterSelected('staff_type', 'Non-academic') ?>>Non-academic</option>
        </select>

        <!-- Month filter -->
        <label>Month: </label>
        <select name="month" class="filter" id="month">
            <option value=""></option>
            <?php for($month = 1; $month <= 12; $month++): ?>
                <option value="<?= $month ?>" <?= isFilterSelected('month', $month) ?>>
                    <?= date("F", mktime(0, 0, 0, $month, 1)) ?>
                </option>
            <?php endfor ?>
        </select>

        <?php if(!empty($_GET)): ?>
            <button id="clear-filter">&times;</button>
        <?php endif ?>
    </div>
</div>

<!-- Staff Record -->
<table>
    <thead>
        <tr>
            <th>Lastname</th>
            <th>Firstname</th>
            <th>Date of Birth</th>
            <th>Gender</th>
            <th>Department</th>
            <th>Staff Type</th>
        </tr>
    </thead>
    <tbody>
        <?php if (!empty($staff_members)): ?>
            <?php foreach($staff_members as $staff): ?>
                <tr>
                    <td><?= $staff['lastname'] ?></td>
                    <td><?= $staff['firstname'] ?></td>
                    <td><?= date("jS F, Y", strtotime($staff['date_of_birth'])) ?></td>
                    <td><?= $staff['gender'] ?></td>
                    <td><?= getDepartment($connection, $staff['department']) ?></td>
                    <td><?= $staff['staff_type'] ?></td>
                </tr>
            <?php endforeach ?>
        <?php endif ?>
    </tbody>
</table>


<!-- Staff Registration Form -->
<div id="registration-modal" class="modal">
    <div class="modal-content">
        <div class="modal-top">
            <span class="close">&times;</span>
        </div>
        <form action="<?= $_SERVER['PHP_SELF'] ?>" method="post">
            <div class="form-row">
                <label for="lastname">Lastname:</label>
                <input type="text" name="lastname" id="lastname" placeholder="Enter your lastname" required>
            </div>
            <div class="form-row">
                <label for="firstname">Firstname:</label>
                <input type="text" name="firstname" id="firstname" placeholder="Enter your firstname" required>
            </div>
            <div class="form-row">
                <label for="dob">Date of Birth:</label>
                <input type="date" name="dob" id="dob" required>
            </div>
            <div class="form-row">
                <label for="gender">Gender:</label>
                <select name="gender" id="gender" required>
                    <option value=""></option>
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                </select>
            </div>
            <div class="form-row">
                <label for="department">Department:</label>
                <select name="department" id="department" required>
                    <option value=""></option>
                    <?php foreach(getDepartments($connection) as $department): ?>
                        <option value="<?= $department['id'] ?>"><?= $department['name'] ?></option>
                    <?php endforeach ?>
                </select>
            </div>
            <div class="form-row">
                <label for="staff_type">Staff Type:</label>
                <select name="staff_type" id="staff_type" required>
                    <option value=""></option>
                    <option value="Academic">Academic</option>
                    <option value="Non-academic">Non-academic</option>
                </select>
            </div>
            <div class="form-row">
                <input type="submit" name="submit" value="Submit">
            </div>
        </form>
    </div>
</div>


<div class="pagination">
    <?php require __DIR__ . "/pagination.php"; ?>
</div>

<?php require __DIR__ . "/footer.php"; ?>
</div>
