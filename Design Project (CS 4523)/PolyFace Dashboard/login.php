<?php
require "db.php";
session_start();
//if the user is already logged in, redirect them back to homepage
if(isset($_SESSION["username"])) {
	echo "You are already logged in. /n";
	echo "You will be redirected in 1 second or click <a href=\"index.html\">here</a>.n";
	header("refresh: 1; index.html");
}
else {
  //if the user have entered both entries in the form, check if they exist in the database
  if(isset($_POST["username"]) && isset($_POST["password"])) {

    //check if entry exists in database
	
	$query="Select username, password, permission from users where username= :uname and password= :pass";
	if($stmt = oci_parse($oci, $query)){
		$password = md5($_POST["password"]);
		$username = $_POST["username"];
		//Bind Params
		oci_bind_by_name($stmt, ':uname', $username);
		oci_bind_by_name($stmt, ':pass', $password);
		//Bind Results
		oci_define_by_name($stmt, 'USERNAME', $user);
		oci_define_by_name($stmt, 'PASSWORD', $pass);
		oci_define_by_name($stmt, 'PERMISSION', $permission);
		
		oci_execute($stmt);
		if(oci_fetch($stmt)){
			$_SESSION["username"] = $user;
			$_SESSION["password"] = $pass;
			$_SESSION["permission"] = $permission;
			$_SESSION["REMOTE_ADDR"] = $_SERVER["REMOTE_ADDR"]; //store clients IP address to help prevent session hijack
			//echo "Login successful. \n";
			//echo "You will be redirected in 1 second or click <a href=\"index.html\">here</a>.";
			header("refresh: 1; index.html");
		}
		else{
			sleep(1); //pause a bit to help prevent brute force attacks
			echo "Your username or password is incorrect, click <a href=\"login.html\">here</a> to try again.";	
		}
		oci_free_statement($stmt);
	} 
  }
  //if not then display login form
  else {
	  header("refresh: 1; login.html");
  }
}
?>
