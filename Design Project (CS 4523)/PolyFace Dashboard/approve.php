<?php

	require "db.php";
	$eventArr = $_POST['event'];
	if(empty($eventArr)){
		$noSubmit = "You did not submit anything.";
		$_SESSION['noSubmit']= $noSubmit;
		header("refresh: 2; index.html");
	}
	else{
		$arraySize = count($eventArr);
		for($i=0; $i<$arraySize; $i++){
			$query="UPDATE event SET ISPUBLISHED='1' WHERE eventID = :eventID";
			$eventID = $eventArr[$i];
			if($stmt = oci_parse($oci, $query)){
				oci_bind_by_name($stmt, ':eventID', $eventID);
				oci_execute($stmt);
			}
		}
		oci_free_statement($stmt);
		header("refresh: 1; index.html");
	}