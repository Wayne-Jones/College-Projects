<?php
	session_start();
	if(isset($_POST["fname"])){
		if($_POST["fname"]!=""){
			$_SESSION["fname"] = $_POST["fname"];
		}
		else{
			$infoNotSet = "One of the fields are missing";
			$_SESSION["infoNotSet"]= $infoNotSet;
		}
	}
	if(isset($_POST["lname"])){
		if($_POST["lname"]!=""){
			$_SESSION["lname"] = $_POST["lname"];
		}
		else{
			$infoNotSet = "One of the fields are missing";
			$_SESSION["infoNotSet"]= $infoNotSet;
		}
	}
	if(isset($_POST["address"])){
		if($_POST["address"]!=""){
			$_SESSION["address"] = $_POST["address"];
		}
		else{
			$infoNotSet = "One of the fields are missing";
			$_SESSION["infoNotSet"]= $infoNotSet;
		}
	}
	if(isset($_POST["phone"])){
		if($_POST["phone"]!=""){
			$_SESSION["phone"] = $_POST["phone"];
		}
		else{
			$infoNotSet = "One of the fields are missing";
			$_SESSION["infoNotSet"]= $infoNotSet;
		}
	}
	if(isset($_POST["email"])){
		if($_POST["email"]!=""){
			$_SESSION["email"] = $_POST["email"];
		}
		else{
			$infoNotSet = "One of the fields are missing";
			$_SESSION["infoNotSet"]= $infoNotSet;
		}
	}
	if(isset($_POST["creditCard"])){
		if($_POST["creditCard"]!=""){
			$_SESSION["creditCard"] = $_POST["creditCard"];
		}
		else{
			$infoNotSet = "One of the fields are missing";
			$_SESSION["infoNotSet"]= $infoNotSet;
		}
	}

?>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Shopping Cart - Personal Info</title>
<link href="scstyle.css" rel="stylesheet" type="text/css">
</head>

<body>

<div class="container">
  <div class="header" align="center"><h1>Shopping Cart - Personal Info</h1> 
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
  <?php
  	if(!isset($_SESSION["fname"],$_SESSION["lname"],$_SESSION["address"],$_SESSION["phone"],$_SESSION["email"],$_SESSION["creditCard"])){
		echo "Please fill out the following information! <br/>";
		echo '<form action="personalinfo.php" method="post"><br/>
		First Name: <input type="text" name="fname"><br/>
		Last Name: <input type="text" name="lname"><br/>
		Address: <input type="text" name="address"><br/>
		Phone Number: <input type="text" name="phone"><br/>
		Email: <input type="text" name="email"><br/>
		Credit Card: <input type="text" name="creditCard"><br/>
		<input type="submit" name="submit">
		</form>';
	}
	else if(isset($_SESSION["infoNotSet"])){
		unset($_SESSION["infoNotSet"]);
		echo "We are missing some information Please fill out the following information. <br/>";
		echo '<form action="personalinfo.php" method="post"><br/>
		First Name: <input type="text" name="fname"><br/>
		Last Name: <input type="text" name="lname"><br/>
		Address: <input type="text" name="address"><br/>
		Phone Number: <input type="text" name="phone"><br/>
		Email: <input type="text" name="email"><br/>
		Credit Card: <input type="text" name="creditCard"><br/>
		<input type="submit" name="submit">
		</form>';
	}
	else{
		echo "Welcome Back! Here is your information. <br/><br/>";
		echo 'First Name: '.$_SESSION["fname"].' <br/>';
		echo 'Last Name: '.$_SESSION["lname"].' <br/>';
		echo 'Address: '.$_SESSION["address"].' <br/>';
		echo 'Phone: '.$_SESSION["phone"].' <br/>';
		echo 'Email: '.$_SESSION["email"].' <br/>';
		echo 'Credit Card: '.$_SESSION["creditCard"].' <br/><br/>';
		echo 'If you would like to update your profile, please complete the form below<br/>';
		echo '<form action="personalinfo.php" method="post">
		First Name: <input type="text" name="fname"><br/>
		Last Name: <input type="text" name="lname"><br/>
		Address: <input type="text" name="address"><br/>
		Phone Number: <input type="text" name="phone"><br/>
		Email: <input type="text" name="email"><br/>
		Credit Card: <input type="text" name="creditCard"><br/>
		<input type="submit" name="submit">
		</form>';
			
	}
  ?>
    <!-- end .content --></div>
  <div class="footer">
    <p>Shopping Cart Created by: Wayne Jones</p>
    <!-- end .footer --></div>
  <!-- end .container --></div>
</body>
</html>