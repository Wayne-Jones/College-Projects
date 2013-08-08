// BlackJack Program

var numWins = getCookie("blackjackNum") ? getCookie("blackjackNum") : 0;
function playBlackJack(){
	var card1 = Math.floor(Math.random()*13)+2;
	var card2 = Math.floor(Math.random()*13)+2;
	with(document.forms[0]){
		c1.src = "images/" + card1 + "hearts.gif";
		c2.src = "images/" + card2 + "hearts.gif";
		if(card1 == 11 && card2 == 11){
			//If Both Cards have are aces, make one value 11 and the other 1
			card1 = 11;
			card2 = 1;	
		}
		if(card1 > 11){
			//If card 1 is a King, Queen or Jack change the value of the card 1 to 10
			card1 = 10;
		}
		if(card2 > 11){
			//If card 2 is a King, Queen or Jack change the value of the card 2 to 10
			card2 = 10;
		}
		if(c1.src == c2.src){
			identical.value++;	
		}	
		var handTotal = card1 + card2;
		firstCard.value = card1;
		secondCard.value = card2;
		total.value = handTotal;
		hand.value++;
		if(handTotal == 21){
			//If the hand is a blackjack, update the cookie
			var now = new Date();
			now.setTime(now.getTime() + 365 * 24 * 60 * 60 * 1000);
			setCookie("blackjackNum", ++numWins, now);
			setTimeout("openWin()", 2000);
		}
		blackjackNum.value = numWins;
	}
} //End PlayBlackJack
function openWin(){
	newWindow = window.open("winScreen.html", "Win Screen", "width=350,height=400");	
}
function setCookie(name, value, expires, path, domain, secure) {
  var curCookie = name + "=" + escape(value) +
      ((expires) ? "; expires=" + expires.toGMTString() : "") +
      ((path) ? "; path=" + path : "") +
      ((domain) ? "; domain=" + domain : "") +
      ((secure) ? "; secure" : "");
  document.cookie = curCookie;
} //End SetCookie
function getCookie(name) {
  var dc = document.cookie;
  var prefix = name + "=";
  var begin = dc.indexOf("; " + prefix);
  if (begin == -1) {
    begin = dc.indexOf(prefix);
    if (begin != 0) return null;
  } else
    begin += 2;
  var end = document.cookie.indexOf(";", begin);
  if (end == -1)
    end = dc.length;
  return unescape(dc.substring(begin + prefix.length, end));
} //End GetCookie
function deleteCookie(name, path, domain) {
  if (getCookie(name)) {
    document.cookie = name + "=" + 
    ((path) ? "; path=" + path : "") +
    ((domain) ? "; domain=" + domain : "") +
    "; expires=Thu, 01-Jan-70 00:00:01 GMT";
  }
} //End DeleteCookie
function fixDate(date) {
  var base = new Date(0);
  var skew = base.getTime();
  if (skew > 0)
    date.setTime(date.getTime() - skew);
} //End FixDate