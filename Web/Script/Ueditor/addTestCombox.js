var objItems = [{
    id: 1,
    name: '测试1',
    value:'#test1#'
}, {
    id: 2,
    name: '测试2',
    value: '#test2#'
}, {
    id: 3,
    name: '测试3',
    value: '#test3#'
}];

UE.registerUI('replacekey', function(editor, uiName) {
    //注册按钮执行时的command命令,用uiName作为command名字，使用命令默认就会带有回退操作
    editor.registerCommand(uiName, {
        execCommand: function(cmdName, item) {
            var html = '<input style="border:#6CF solid 1px;" truevalue="' + item.value +
                '" value="' + item.label + '" type="button" />';
            this.execCommand('insertHtml', html);
        }
    });
    //创建下拉菜单中的键值对，这里我用字体大小作为例子
    var items = [];
    for (var i = 0; i < objItems.length; i++) {
        var item = objItems[i];
        items.push({
            //显示的条目
            label: item['name'],
            //选中条目后的返回值
            key: item['id'],
            value: item['value']
        });
    }
    //创建下拉框
    var combox = new UE.ui.Combox({
        //需要指定当前的编辑器实例
        editor: editor,
        //添加条目
        items: items,
        //当选中时要做的事情
        onselect: function(t, index) {
            //拿到选中条目的值
            // editor.execCommand(uiName, this.items[index]);
            //t.setValue(t.initValue);
            var newHtml = "<span style='color:red' class=" + this.items[index].value + ">" + this.items[index].label + "</span>";
            UE.getEditor('editor').execCommand('insertHtml', newHtml);
        },
        //提示
        title: '项目变量',
        //当编辑器没有焦点时，combox默认显示的内容
        initValue: '项目变量'
    });
    return combox;
});