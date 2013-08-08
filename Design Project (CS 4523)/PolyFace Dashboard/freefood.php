<?php 

include "include.php"

if($stmt = $mysqli->prepare(" select NAMEOFEVENT, TIMEOFEVENT, PLACEOFEVENT, EVENTDESCRIPTION from Event where TYPEOFEVENT = 'freefood'"))
{
	$stmt->execute();
	$stmt->bind_result($name,$time,$type,$description);

?>