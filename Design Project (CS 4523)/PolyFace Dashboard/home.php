<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<?php
require "db.php";
session_start();
?>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>PolyFace Dashboard</title>
</head>

<body>
<div class="nav">
    <ul>
    <li><a href="home.php" target="_parent">Home</a></li>
    <?php
    if(isset($_SESSION["username"])){
		echo '<li><a href="logout.php" target="_parent">Logout</a></li>';}
	else{
		echo '<li><a href="login.php" target="_parent">Login</a></li>';}
	?>
    </ul>
</div>
</body>
</html>