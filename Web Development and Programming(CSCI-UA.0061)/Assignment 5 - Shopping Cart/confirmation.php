<?php
session_start();
if (!isset($_SESSION["fname"],$_SESSION["lname"],$_SESSION["address"],$_SESSION["phone"],$_SESSION["email"],$_SESSION["creditCard"])) {
	?>
	<script type="text/javascript">
	alert("Please complete the personal information page before proceeding");
	window.location.href = "./personalinfo.php"
	</script>
	<?php
}
if ($_SESSION['total'] == 0) {
	?>
	<script type="text/javascript">
	alert("Please select your products");
	window.location.href= "./productchoice.php"
	</script>
    <?php
}
?>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Shopping Cart - Confirmation</title>
<link href="scstyle.css" rel="stylesheet" type="text/css">
</head>

<body>

<?php
	if(isset($_POST["album1"])){
		if($_POST["album1"]!=""){
			$_SESSION["album1"] = $_POST["album1"];
		}
	}
	if(isset($_POST["album2"])){
		if($_POST["album2"]!=""){
			$_SESSION["album2"] = $_POST["album2"];
		}
	}
	if(isset($_POST["album3"])){
		if($_POST["album3"]!=""){
			$_SESSION["album3"] = $_POST["album3"];
		}
	}
	if(isset($_POST["album4"])){
		if($_POST["album4"]!=""){
			$_SESSION["album4"] = $_POST["album4"];
		}
	}
	if(isset($_POST["album5"])){
		if($_POST["album5"]!=""){
			$_SESSION["album5"] = $_POST["album5"];
		}
	}
	if(isset($_POST["album6"])){
		if($_POST["album6"]!=""){
			$_SESSION["album6"] = $_POST["album6"];
		}
	}
	if(isset($_POST["album7"])){
		if($_POST["album7"]!=""){
			$_SESSION["album7"] = $_POST["album7"];
		}
	}
	if(isset($_POST["album8"])){
		if($_POST["album8"]!=""){
			$_SESSION["album8"] = $_POST["album8"];
		}
	}
	if(isset($_POST["total"])){
		if($_POST["total"]!=""){
			$_SESSION["total"] = $_POST["total"];
		}
	}
	
	?>

<div class="container">
  <div class="header" align="center"><h1>Shopping Cart - Confirmation</h1> 
    <!-- end .header --></div>
  <div class="sidebar1">
    <ul class="nav">
      <li><a href="personalinfo.php">Personal Info</a></li>
      <li><a href="productchoice.php">Product Choice</a></li>
      <li><a href="confirmation.php">Confirmation</a></li>
      <li><a href="order.php">Order</a></li>
      <li><a href="reset.php">Reset Order Info</a></li>
    </ul>
    <!-- end .sidebar1 --></div>
  <div class="content">
  <form action="order.php" method="post">
  <?php
	echo "<h3 align='center'>Here is a summary of your order!</h3>";	
	echo "<table>";
	echo "<tbody>";
	echo "<tr>";
	if($_SESSION["album1"]>0) {
		echo "<td align='center'>";
		echo "<img src='productImages/Best_Hit_AKG.jpg' width='220px' height='220px' border='1'><br/>";
		echo "Best Hit AKG Album Quantity: ".$_SESSION["album1"]."<br>";
		echo "</td>";
	} 
	if($_SESSION["album2"]>0) {
		echo "<td align='center'>";
		echo "<img src='productImages/Fanclub_Cover.jpg' width='220px' height='220px' border='1'><br/>";
		echo "Fan Club Album Quantity: ".$_SESSION["album2"]."<br>";
		echo "</td>";
	}
	if($_SESSION["album3"]>0) {
		echo "<td align='center'>";
		echo "<img src='productImages/Kimi_Tsunagi_Five_M_Cover.jpg' width='220px' height='220px' border='1'><br/>";
		echo "Kimi Tsunagi Five M Album Quantity: ".$_SESSION["album3"]."<br>";
		echo "</td>";
	}
	echo "</tr>";
	echo "<tr>";
	if($_SESSION["album4"]>0) {
		echo "<td align='center'>";
		echo "<img src='productImages/Landmark_Cover.png' width='220px' height='220px' border='1'><br/>";
		echo "Landmark Album Quantity: ".$_SESSION["album4"]."<br>";
		echo "</td>";
	}
	if($_SESSION["album5"]>0) {
		echo "<td align='center'>";
		echo "<img src='productImages/Magic_Disk_Cover.jpg' width='220px' height='220px' border='1'><br/>";
		echo "Magic Disk Album Quantity: ".$_SESSION["album5"]."<br>";
		echo "</td>";
	}
	if($_SESSION["album6"]>0) {
		echo "<td align='center'>";
		echo "<img src='productImages/Sol-fa_Cover.jpg' width='220px' height='220px' border='1'><br/>";
		echo "Sol-Fa Album Quantity: ".$_SESSION["album6"]."<br>";
		echo "</td>";
	}
	echo "</tr>";
	echo "<tr>";
	if($_SESSION["album7"]>0) {
		echo "<td align='center'>";
		echo "<img src='productImages/Surf_Bungaku_Kamakura_Cover.jpg' width='220px' height='220px' border='1'><br/>";
		echo "Surf Bungaku Kamakura Album Quantity: ".$_SESSION["album7"]."<br>";
		echo "</td>";
	}
	if($_SESSION["album8"]>0) {
		echo "<td align='center'>";
		echo "<img src='productImages/World_world_world_cover.jpg' width='220px' height='220px' border='1'><br/>";
		echo "World World World Album Quantity: ".$_SESSION["album8"]."<br>";
		echo "</td>";
	}
	echo "</tbody>";
	echo "</table>";
	?>
	<p align="center">
	<input type="submit" value="Confirm Your Order"></p>
	</form>
	<form action="productchoice.php">
    <p align="center">
	<input type="submit" value="Adjust Order"></p>
	</form>

    <!-- end .content --></div>
  <div class="footer">
    <p>Shopping Cart Created by: Wayne Jones</p>
    <!-- end .footer --></div>
  <!-- end .container --></div>
</body>
</html>