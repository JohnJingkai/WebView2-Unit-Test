function sendThisItem(item)
{
    window.chrome.webview.postMessage(item);
}
function ShowWinFormsMessage1(ary) {
    //ary = ["NAME", "DOB", "SSN", "GENDER"];

    var res = '{"';
    for (var i = 0; i < ary.length; i++) {
        var val = document.getElementById(ary[i]).value
        //alert(typeof (obj[i]));
        res += ary[i];
        res += '":"';
        res += val;
        res += '","';
    }
    res = res.substring(0, res.length - 2);
    res += "}";
    alert(res);
    sendThisItem(res);
}
function renderDataInTheTable(todos) {
    alert(todos);
    const mytable = document.getElementById("html-data-table");
    todos.forEach(todo => {
        let newRow = document.createElement("tr");
        newRow.className = "success";
        //newRow.bgColor = '#33FF86';
        //newRow.className = "success";
        Object.values(todo).forEach((value) => {
            let cell = document.createElement("td");
            cell.innerText = value;
            newRow.appendChild(cell);
        })
        mytable.appendChild(newRow);

    });

}