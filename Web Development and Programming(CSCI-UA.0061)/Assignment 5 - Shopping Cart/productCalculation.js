var total = 0;
var album1 = 0;
var album2 = 0;
var album3 = 0;
var album4 = 0;
var album5 = 0;
var album6 = 0;
var album7 = 0;
var album8 = 0;

function calcTotal(){
	
	var album1 =  document.productsForm.album1.value;
	var album2 =  document.productsForm.album2.value;
	var album3 =  document.productsForm.album3.value;
	var album4 =  document.productsForm.album4.value;
	var album5 =  document.productsForm.album5.value;
	var album6 =  document.productsForm.album6.value;
	var album7 =  document.productsForm.album7.value;
	var album8 =  document.productsForm.album8.value;
	var total = (album1 * 15)+(album2 * 10)+(album3 * 10)+(album4 * 10)+(album5 * 10)+(album6 * 10)+(album7 * 10) + (album8 * 10);
	document.getElementById("total").value = total;
	
}