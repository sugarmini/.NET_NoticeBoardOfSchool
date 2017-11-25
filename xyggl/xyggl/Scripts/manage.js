window.onload = function () {
    var oGroup = document.getElementById('group');
    var aInp = oGroup.getElementsByTagName('a');
    var aLi = oGroup.getElementsByTagName('li');
    var oContent = document.getElementById('content');
	var aTable = oContent.getElementsByTagName('table');
	var oAdd = document.getElementById('add-bulletin');

	hideall();
	green(aLi[0]);
	show(aTable[0]);
    function hideall() {
        for(var i = 0; i < aTable.length; i++){
            aTable[i].style.display = 'none';
            aLi[i].style.borderBottom = '3px solid #fff';
		}
		oAdd.style.display = 'none';
    }
    function show(a) {
        a.style.display = 'block';
    }
    function green(a) {
        a.style.borderBottom = '3px solid #0a8e6c';
    }
    for(var i = 0; i < aInp.length ; i++){
        aInp[i].index = i;
        aInp[i].onclick = function () {
            hideall();
            if(this.index === 1){
                show(oAdd);
            }
            show(aTable[this.index]);
            green(aLi[this.index]);
        }
    }
}