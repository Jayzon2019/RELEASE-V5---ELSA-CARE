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


function imageUpload(input, placeholderId, previewId, messageId)
{
	if (input.files.length > 0)
	{
		const file = input.files[0];
		const reader = new FileReader();

		reader.onloadend = (event) =>
		{
			const uploadedData = event.target.result;
			const image = new Image();

			const placeholder = document.getElementById(placeholderId);
			const preview = document.getElementById(previewId);
			const message = document.getElementById(messageId);

			image.onload = (event) =>
			{
				console.log('Valid image file');
				input.classList.remove('invalid');
				placeholder.value = uploadedData;
				preview.src = uploadedData;
				message.style.display = 'none';
			}

			image.onerror = (event) =>
			{
				console.log('Invalid image file');
				input.classList.add('invalid');
				placeholder.value = '';
				preview.src = '';
				message.style.display = 'inline';
			}

			image.src = uploadedData;
		}

		reader.readAsDataURL(file);
	}
}


function documentUpload(input, placeholderId, messageId)
{
	if (input.files.length > 0)
	{
		const file = input.files[0];
		const reader = new FileReader();

		reader.onloadend = (event) =>
		{
			const uploadedData = event.target.result;
			const placeholder = document.getElementById(placeholderId);
			const message = document.getElementById(messageId);

			const data = uploadedData.split(',')[1];

			console.log('data');
			console.log(data);

			console.log('Valid document file');
			input.classList.remove('invalid');
			placeholder.value = data;
			message.style.display = 'none';
		}

		console.log('file');
		console.log(file);
		reader.readAsDataURL(file);
	}
}

