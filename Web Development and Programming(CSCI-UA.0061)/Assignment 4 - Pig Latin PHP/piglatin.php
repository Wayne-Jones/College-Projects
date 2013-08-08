<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>PHP Pig Latin</title>
<link href="piglatinstyle.css" rel="stylesheet" type="text/css">
</head>

<body>

<?php
echo '<div id="wrapper">
    <div id="header"></div>
    <div id="container">
    <div class="results">';
$userInput = $_POST["userInput"];
$language = $_POST["language"];
translate($userInput, $language);



function translate($userInput, $language){
	$inputArr = explode(" ", $userInput);
	$charAppend = array();
	if($language=="Pig Latin"){
		echo "<h3>Here is what you entered: </h3>";
		echo $userInput;
		echo "<h3>Here is the Pig Latin Translation: </h3>";
		for($i=0; $i<count($inputArr); $i++){
			pigLatin($inputArr[$i], $charAppend);
		}
	}
	else{
		echo "<h3>Here is your English output: </h3>";
		echo $userInput;
	}
}
function pigLatin($string, $charAppend){
	$vowelArray = array("a","e","i","o","u");
	$isVowel=0;
	for($i=0; $i<count($vowelArray);$i++){
		if($vowelArray[$i]==strtolower($string[0])){
			$isVowel=1;
			if($charAppend[0]==""){
				echo "$string"."-way"." ";
			}
			else{
				$appendString = implode($charAppend);
				echo "$string"."-"."$appendString"."ay"." ";
			}
		}
	}
	if($isVowel==0){
		array_push($charAppend, $string[0]);
		pigLatin(substr($string, 1, strlen($string)-1), $charAppend);
	}
}
echo '</div>
    </div>
    <div id="footer"></div>
</div>';
?>
</body>
</html>