function collapse(objid) {
    var obj = document.getElementById(objid);
    if (obj.style.display != 'none')
        collapseAll();
    else {
        collapseAll();
        obj.style.display = '';
    }
}
function collapseAll() {
    for (var i = 1; i <= 10; i++) {
        var obj = document.getElementById('d_' + i.toString());
        if (obj) obj.style.display = 'none';
    }
}
function expandAll() {
    for (var i = 1; i <= 10; i++) {
        var obj = document.getElementById('d_' + i.toString());
        if (obj) obj.style.display = '';
    }
}