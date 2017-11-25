window.onload = function () {
	//»ÃµÆÆ¬
	var oImg = document.getElementById('img');
	var oPrev = document.getElementById('prev');
	var oNext = document.getElementById('next');
	var oDot = document.getElementById('dot');
	var aLi = oDot.getElementsByTagName('li');

	var arr = ["/Image/classroom.jpg", "/Image/corridor.jpg", "/Image/library.jpg"];
	var length = arr.length;
	var num = 0;
	var timer = null;
	oImg.src = arr[num];

	function green(a) {
		a.style.background = '#0a8e6c';
	}
	function clear() {
		for (var i = 0; i < length; i++) {
			aLi[i].style.background = '#fff';
		}
	}

	for (var i = 0; i < length; i++) {
		var li = document.createElement("li");
		oDot.appendChild(li);

		aLi[i].index = i;
		aLi[i].onclick = function () {
			oImg.src = arr[this.index];
			clear();
			green(this);
		}
	}
	green(aLi[0]);

	oPrev.onclick = function () {
		num--;
		if (num < 0) {
			num = length - 1;
		}
		oImg.src = arr[num];
	};
	oNext.onclick = function () {
		num++;
		if (num === length) {
			num = 0;
		}
		oImg.src = arr[num];
	};

	function play() {
		timer = setInterval(function () {
			num++;
			if (num === length) {
				num = 0;
			}
			oImg.src = arr[num];
			clear();
			green(aLi[num]);
		}, 3000);
	}
	play();

	oImg.onmouseover = function () {
		clearInterval(timer);
	};
	oImg.onmouseout = play;

	//·ÖÀà
	var oClassify = document.getElementById('classify');
	var oMenu = document.getElementById('menu');
	var aTag = oClassify.getElementsByTagName('a');
	var aUl = oMenu.getElementsByTagName('ul');

	function show(a) {
		a.style.display = 'block';
	}
	function hideall() {
		for (var i = 0; i < aUl.length; i++) {
			aUl[i].style.display = 'none';
		}
	}
	function borderbottom(a) {
		a.style.borderBottom = '3px solid #0a8e6c';
	}
	hideall();
	show(aUl[0]);
	borderbottom(aTag[0]);
	for (var i = 0; i < aTag.length; i++) {
		aTag[i].index = i;
		aTag[i].onclick = function () {
			for (var i = 0; i < aUl.length; i++) {
				hideall();
				aTag[i].style.borderBottom = '';
			}
			show(aUl[this.index]);
			borderbottom(this);
		}
	}
}