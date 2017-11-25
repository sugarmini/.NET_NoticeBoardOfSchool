$('.auth').data({ 's': 0 });

$('input[name=email]').blur(function () {
	val = this.value;

	if (!val.match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) {
		$(this).data({ 's': 0 });
		$(this).next().show();
	}
	else {
		$(this).data({ 's': 1 });
		$(this).next().hide();
	}
});

$('#pwd').blur(function () {
	val = this.value;
	if (val.length < 6 || val.length > 20) {
		$(this).data({ 's': 0 });
		$(this).next().show();
	}
	else {
		$(this).data({ 's': 1 });
		$(this).next().hide();
	}
});

$('#repwd').blur(function () {
	val1 = $('#pwd').val();
	val2 = this.value;

	if (val1 != val2) {
		$(this).data({ 's': 0 });
		$(this).next().show();
		alert(val1);
	}
	else {
		$(this).data({ 's': 1 });
		$(this).next().hide();
	}
});


$('.signup-btn').click(function () {
	$('input.auth').blur();
	tot = 0;
	$('.auth').each(function () {
		tot += $(this).data('s');
	});
	if (tot != 3) {
		return false;
	}
});