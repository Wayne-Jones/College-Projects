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
<title>Shopping Cart - Order Page</title>
<link href="scstyle.css" rel="stylesheet" type="text/css">
</head>

<body>

<div class="container">
  <div class="header" align="center"><h1>Shopping Cart - Order Page</h1> 
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
  <h2><?php echo $_SESSION['fname'];?>,</h2><br/>
  <h3 align='center'>Here is the confirmation of your order!</h3><br/>
  <h2>Customer Information:</h2>
    <ul>
    <li>Full Name: <?php echo $_SESSION['fname']; echo " "; echo $_SESSION['lname'];?></li>
    <li>Your Address: <?php echo $_SESSION['address'];?></li>
    <li>Your Email Address: <?php echo $_SESSION['email'];?></li>
    <li>Your Phone: <?php echo $_SESSION['phone'];?></li>
    <li>Credit Card: <?php echo $_SESSION['creditCard'];?></li>
    </ul>
  
  <?php		
	echo "<table>";
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
	echo "</table>";
	?>
    <!-- end .content --></div>
  <div class="footer">
    <p>Shopping Cart Created by: Wayne Jones</p>
    <!-- end .footer --></div>
  <!-- end .container --></div>
</body>
</html>