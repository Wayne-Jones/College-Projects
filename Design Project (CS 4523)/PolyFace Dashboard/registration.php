<?php
    require 'db.php';
	session_start();
	
if(isset($_POST["username"]) && isset($_POST["password"])) 
  {
	
    //check if email already exists in database
	$query="Select username from users where username= :uname";
	if($stmt = oci_parse($oci, $query)){
		$username = $_POST["username"];
		oci_bind_by_name($stmt, ':uname', $username);
		oci_execute($stmt);
		if(oci_fetch($stmt)){
			$similarUser = "Similar user with this name";
			$_SESSION["similarUser"]= $similarUser;
			header("refresh: 1; registration.html");
		}
		oci_free_statement($stmt);
	}
    if(!isset($_SESSION["similarUser"])){
		//insert the person into database
		$password = md5($_POST["password"]);
		$query="INSERT INTO users(username,password,permission) values (:uname,:pass, :permission)";
		if ($stmt = oci_parse($oci, $query)) 
		{
			$permission = 0;
			oci_bind_by_name($stmt, ':uname', $username);
			oci_bind_by_name($stmt, ':pass', $password);
			oci_bind_by_name($stmt, ':permission', $permission);
			oci_execute($stmt);
			oci_free_statement($stmt);
			$registered = "Registration Complete";
			$_SESSION["registered"] = $registered;
			header("refresh: 1; registration.html");
		}
	}
  }
  //if not then display registration form
  else 
  {
	  echo "Please complete all fields of the registration form";
	  echo "REDIRECTING to the registration page";
	  header("refresh: 3; registration.html");
  }
?>
        