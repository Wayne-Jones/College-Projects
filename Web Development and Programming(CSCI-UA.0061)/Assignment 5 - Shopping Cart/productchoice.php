<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Shopping Cart - Product Choice</title>
<link href="scstyle.css" rel="stylesheet" type="text/css">
<script src="productCalculation.js" type="text/javascript"></script>
</head>

<body>

<div class="container">
  <div class="header" align="center"><h1>Shopping Cart - Product Choice</h1>
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
  	<h3>Welcome to the Products page. Select the number of Asian Kung-Fu Generation Albums, you would like to purchase.</h3><br/>
  	<form name="productsForm" action="confirmation.php" method="POST">
    <table>
    <tbody>
    <tr>
        <td align="center"><img src="productImages/Best_Hit_AKG.jpg" border="1px" width="220px" height="220px"><br><p>Best Hit AKG Album $15</p><input type="text" name="album1" id="album1" value="" onChange="calcTotal()"></td>
        <td align="center"><img src="productImages/Fanclub_Cover.jpg"  border="1px" width="220px" height="220px"><br><p>Fanclub Album $10</p><input type="text" name="album2" id="album2" value="" onChange="calcTotal()"></td>
        <td align="center"><img src="productImages/Kimi_Tsunagi_Five_M_Cover.jpg"  border="1px" width="220px" height="220px"><br><p>Kimi Tsunagi Five M Album $10</p><input type="text" name="album3" id="album3" value="" onChange="calcTotal()"></td>
    <tr>
        <td align="center"><img src="productImages/Landmark_Cover.png"  border="1px" width="220px" height="220px"><br><p>Landmark Album $10</p><input type="text" name="album4" id="album4" value="" onChange="calcTotal()"></td>
        <td align="center"><img src="productImages/Magic_Disk_Cover.jpg"  border="1px" width="220px" height="220px"><br><p>Magic Disk Album $10</p><input type="text" name="album5" id="album5" value="" onChange="calcTotal()"></td>
        <td align="center"><img src="productImages/Sol-fa_Cover.jpg"  border="1px" width="220px" height="220px"><br><p>Sol-Fa Album $10</p><input type="text" name="album6" id="album6" value="" onChange="calcTotal()"></td>
    </tr>
    <tr>
        <td align="center"><img src="productImages/Surf_Bungaku_Kamakura_Cover.jpg"  border="1px" width="220px" height="220px"><br><p>Surf Bungaku Kamakura Album $10</p><input type="text" name="album7" id="album7" value="" onChange="calcTotal()"></td>
        <td align="center"><img src="productImages/World_world_world_cover.jpg"  border="1px" width="220px" height="220px"><br><p>World World World Album $10</p><input type="text" name="album8" id="album8" value="" onChange="calcTotal()"></td>
        </tr>
    </tbody>
    </table>
    <br/><br/>
    <p align="center">Total Price so far: <input type="text" name="total" id="total" value "" onChange="calcTotal()">
    <input value="Check Out Here!" type="submit"></p>
    <script type="text/javascript">
			document.getElementById("total").value= total;

			document.getElementById("album1").value= album1;
			document.getElementById("album2").value= album2;

			document.getElementById("album3").value= album3;

			document.getElementById("album4").value= album4;

			document.getElementById("album5").value= album5;

			document.getElementById("album6").value= album6;

			document.getElementById("album7").value= album7;

			document.getElementById("album8").value= album8;

			</script>
    </form>
    <!-- end .content --></div>
  <div class="footer">
    <p>Shopping Cart Created by: Wayne Jones</p>
    <!-- end .footer --></div>
  <!-- end .container --></div>
</body>
</html>