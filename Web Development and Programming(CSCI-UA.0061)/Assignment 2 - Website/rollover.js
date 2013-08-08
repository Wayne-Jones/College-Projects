// JavaScript Document
window.onload = rolloverAction;

function rolloverAction() {
	for (var i=0; i<document.images.length; i++) {
		if (document.images[i].parentNode.tagName == "LI") {
			setupRollover(document.images[i]);
		}
	}
}

function setupRollover(image) {
	image.overImage = new Image();
	image.outImage = new Image();
	image.outImage.src = image.src;
	image.onmouseout = rollOut;

	image.clickImage = new Image();
	image.clickImage.src = "images/" + image.id + ".jpg";
	image.onclick = rollClick;	

	image.overImage.src = "images/rollover.gif";
	image.onmouseover = rollOver;	
}

function rollOver() {
	this.src = this.overImage.src;
}

function rollOut() {
	this.src = this.outImage.src;
}

function rollClick() {
	this.src = this.clickImage.src;
}