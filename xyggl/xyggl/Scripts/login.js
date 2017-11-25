window.onload = function (){
    var oLogin = document.getElementById('login');
    var oSignup = document.getElementById('signup');
    var oBox1 = document.getElementById('box1');
    var oBox2 = document.getElementById('box2');
    var oEmail = document.getElementById('email');
    var oUser = document.getElementById('user');
    var oPwd = document.getElementById('pwd');
    var oPsd = document.getElementById('psd');
    var oRepwd = document.getElementById('repwd');
    var oVerify = document.getElementById('verify');
    var oEnter = document.getElementById('enter');
    var aError = document.getElementsByClassName('error');

    hide(oBox2);
    green(oLogin);

    function show(a) {
        a.style.display = 'block';
    }
    function hide(a) {
        a.style.display = 'none';
    }
    function green(a) {
        a.style.color = '#0a8e6c';
    }
    function black(a) {
        a.style.color = '#000';
    }
    oLogin.onclick = function () {
        show(oBox1);
        hide(oBox2);
        green(oLogin);
        black(oSignup);
    };
    oSignup.onclick = function () {
        hide(oBox1);
        show(oBox2);
        green(oSignup);
        black(oLogin);
    };

    oUser.onblur = function () {
        if(!this.value){
            show(aError[0]);
            i = 1;
        }
        else{
            hide(aError[0]);
            i = 0;
        }
    };
    oPsd.onblur = function () {
        if(!this.value){
            show(aError[1]);
            j = 1;
        }
        else{
            hide(aError[1]);
            j = 0;
        }
    };
    oEnter.onclick = function () {
        oUser.onblur();
        oPsd.onblur();
        if(i+j){
            return false;
        }
    };

    function isEmail(str){
        var reg = /^([a-zA-Z0-9\._-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
        return reg.test(str);
    }
    oEmail.onblur = function () {
        s = this.value;
        if(!isEmail(s)){
            show(aError[2]);
            l = 1;
        }
        else{
            hide(aError[2]);
            l = 0;
        }
    };
    oPwd.onblur = function () {
        a = this.value;
        if(a.length < 6 || a.length > 20){
            show(aError[3]);
            m = 1;
        }
        else{
            hide(aError[3]);
            m = 0;
        }
    };
    oRepwd.onblur = function () {
        b = this.value;
        if(a !== b){
            show(aError[4]);
            n = 1;
        }
        else{
            hide(aError[4]);
            n = 0;
        }
    };
    oVerify.onclick = function () {
        oEmail.onblur();
        oPwd.onblur();
        oRepwd.onblur();
        sum = l + m + n;
        if(sum){
            return false;
        }
	}
	if ("@ViewBag.remain" == "on") {
		alert("de");
		document.getElementById("auto").checked = true;
	}
	else
		document.getElementById("auto").checked = false;
}