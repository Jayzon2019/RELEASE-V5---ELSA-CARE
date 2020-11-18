// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function idleAutoLogout()
{
	var timer;

	window.onload = resetTimer;
	window.onmousemove = resetTimer; // catches mouse movements
	window.onmousedown = resetTimer; // catches mouse movements
	window.onclick = resetTimer;     // catches mouse clicks
	window.onscroll = resetTimer;    // catches scrolling
	window.onkeypress = resetTimer;  //catches keyboard actions

	function logout()
	{
		var base = (document.querySelector('base') || {}).href;
		window.location.href = base + 'home/logout';
	}

	function reload()
	{
		window.location = self.location.href;  //Reloads the current page
	}

	function resetTimer()
	{
		clearTimeout(timer);
		timer = setTimeout(logout, 300000);  // time is in milliseconds (1000 is 1 second)
		//timer = setTimeout(reload, 300000);  // time is in milliseconds (1000 is 1 second)
	}

}

idleAutoLogout();
