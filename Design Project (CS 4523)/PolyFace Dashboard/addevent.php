<?php 

	require "db.php";
	session_start();
	//Upload Image File Script
	$allowedExts = array("gif", "jpeg", "jpg", "png");
	$extension = end(explode(".", $_FILES["file"]["name"]));
	if ((($_FILES["file"]["type"] == "image/gif") || ($_FILES["file"]["type"] == "image/jpeg") || ($_FILES["file"]["type"] == "image/jpg") || ($_FILES["file"]["type"] == "image/pjpeg") || ($_FILES["file"]["type"] == "image/x-png") || ($_FILES["file"]["type"] == "image/png")) && ($_FILES["file"]["size"] < 400000) && in_array($extension, $allowedExts))
	{
		//Checks to see if the file is the correct type, has the correct extension, and checks to see if it's under 400kb
		if ($_FILES["file"]["error"] > 0)
		{
			$fileError = "There was an error uploading your file";
			$_SESSION["fileError"]=$fileError;
			header("refresh:1; index.html");
		}
		else
		{
			$imgPath = "eventpics/".$_FILES["file"]["name"];
			$imgPath = str_replace(' ', '_', $imgPath);	
			if (file_exists($imgPath))
			{
				//Checks to see if the file already exists
				$fileExists="The uploaded file already exists";
				$_SESSION["fileExists"]=$fileExists;
				header("refresh:1; index.html");
			}
			else
			{
				//Move the temp file and save it to the server
				move_uploaded_file($_FILES["file"]["tmp_name"], $imgPath);
			}
		}
		//Execute the query
		$query= "insert into event(NAMEOFEVENT,TYPEOFEVENT,DATEOFEVENT,TIMEOFEVENT,LOCATIONOFEVENT,IMGPATH,ISPUBLISHED,EVENTDESCRIPTION) values(:nameOfEvent, :typeOfEvent, :dateOfEvent, :timeOfEvent, :locationOfEvent, :imgPath, '1', :eventDesc)";
		$name = $_POST["name"];
		$type = $_POST["type"];
		$date = $_POST["date"];
		$time = $_POST["time"];
		$location = $_POST["eventLocation"];
		$description = $_POST["description"];
	
		
		if($stmt = oci_parse($oci,$query)){
			oci_bind_by_name($stmt, ':nameOfEvent', $name);
			oci_bind_by_name($stmt, ':typeOfEvent', $type);
			oci_bind_by_name($stmt, ':dateOfEvent', $date);
			oci_bind_by_name($stmt, ':timeOfEvent', $time);
			oci_bind_by_name($stmt, ':locationOfEvent', $location);
			oci_bind_by_name($stmt, ':imgPath', $imgPath);
			oci_bind_by_name($stmt, ':eventDesc', $description);
			oci_execute($stmt);
		}
		oci_free_statement($stmt);
		$eventPosted="Your event has been posted";
		$_SESSION["eventPosted"]=$eventPosted;
		header("refresh:1; index.html");
	}
	else
	{
		//File does not meet any of the qualifications, display error
		$invalidFile = "Your file does not meet the specified qualifications";
		$_SESSION["invalidFile"]=$invalidFile;
		header("refresh:1; index.html");
	}
	
	
?>