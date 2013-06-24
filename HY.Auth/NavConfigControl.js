

!function () {
    var bis = function () {
        Ext.onReady(function () {
            var columns = [{
                xtype: 'treecolumn',
                text: '标题',
                flex: 3,
                hideable: false,
                dataIndex: 'Title'
            }];
            var fields = [{ name: 'Title', type: 'string' }, { name: 'Url', type: 'string' }];

            //构建列
            !function () {
                for (var i = 0; i < NavConfig.roles.length; i++) {
                    columns.push({
                        xtype: 'checkcolumn',
                        text: NavConfig.roles[i],
                        flex: 1,
                        sortable: false,
                        hideable: false,
                        dataIndex: NavConfig.roles[i]
                    });
                    fields.push({ name: NavConfig.roles[i], type: 'boolean' });
                }
            }();

            //alert(Ext.JSON.encode(fields));

            //构建数据
            var data = {
                "Title": ".",
                "children": []
            };

            !function () {
                for (var i = 0; i < NavConfig.tree.Children.length; i++) {//lv1
                    var c = NavConfig.tree.Children[i];
                    if (c.Title == '') continue;
                    var n = { Title: c.Title, Url: c.Url, children: [], leaf: c.Children.length == 0 };
                    for (var i_r = 0; i_r < c.Roles.length; i_r++) {
                        n[c.Roles[i_r]] = true;
                    }
                    data.children.push(n);

                    for (var ii = 0; ii < c.Children.length; ii++) {
                        var cc = c.Children[ii];
                        if (cc.Title == '') continue;
                        var nn = { Title: cc.Title, Url: cc.Url, children: [], leaf: cc.Children.length == 0 };
                        for (var ii_r = 0; ii_r < cc.Roles.length; ii_r++) {
                            nn[cc.Roles[ii_r]] = true;
                        }
                        n.children.push(nn);

                        for (var iii = 0; iii < cc.Children.length; iii++) {
                            var ccc = cc.Children[iii];
                            if (ccc.Title == '') continue;
                            var nnn = { Title: ccc.Title, Url: ccc.Url, children: [], leaf: ccc.Children.length == 0 };
                            for (var iii_r = 0; iii_r < ccc.Roles.length; iii_r++) {
                                nnn[ccc.Roles[iii_r]] = true;
                            }
                            nn.children.push(nnn);
                        }
                    }
                }
            }();

            //alert(Ext.JSON.encode(data));

            Ext.define('NavModel', {
                extend: 'Ext.data.Model',
                fields: fields
            });

            NavConfig.data = data;

            // 希望使用 proxy[type=memory] 实现重置, 但没成功
            var NavStore = Ext.create('Ext.data.TreeStore', {
                autoLoad: true,
                model: NavModel,
                proxy: {
                    type: 'memory',
                    reader: { type: 'json' }
                },
                root: NavConfig.data
            });

            Ext.create('Ext.tree.Panel', {
                title: '菜单配置',
                height: 400,
                store: NavStore,
                useArrows: true,
                rootVisible: false,
                singleExpand: true,
                width: '100%',
                //columnLines: true,
                rowLines: true,
                columns: columns,
                bbar: [{ xtype: 'tbtext', text: ' ', width: 400 }, {
                    xtype: 'button',
                    text: '保 存',
                    handler: function () {
                        var getRow = function (node) {
                            var row = { Title: node.data.Title, Url: node.data.Url, Roles: [] };
                            for (var j = 0; j < NavConfig.roles.length; j++) {
                                var role = NavConfig.roles[j];
                                if (node.data[role]) {
                                    row.Roles.push(role);
                                }
                            }
                            return row;
                        };

                        var rep = function (nodes) {
                            for (var i = 0; i < nodes.length; i++) {
                                if (nodes[i].childNodes && nodes[i].childNodes.length > 0) {
                                    rep(nodes[i].childNodes);
                                } else {
                                    allrows.push(getRow(nodes[i]));
                                }
                            }
                        };
                        var roots = NavStore.getRootNode().childNodes;
                        var allrows = [];
                        rep(roots);

                        //alert(Ext.JSON.encode(allrows));

                        Ext.Ajax.request({
                            url: '.NavConfig',
                            //method: 'POST',
                            jsonData: allrows,
                            //params: { data: Ext.JSON.encode(allrows) },
                            success: function (response) {
                                var obj = Ext.JSON.decode(response.responseText);
                                if (!obj.error) {
                                    Ext.Msg.alert('成功', '保存成功');
                                }
                            }
                        });
                    }
                }, {
                    xtype: 'button',
                    text: '重 置',
                    handler: function () {
                        window.location.reload();
                        return;
                        var node = NavStore.getRootNode().childNodes[0].data
                        for (var i = 0; i < NavConfig.roles.length; i++) {
                            node[NavConfig.roles[i]] = true;
                        }
                    }
                }],
                renderTo: NavConfig.id
            });

        });
    };

    var check = function () {
        return Ext ? true : false;
    };

    var wait = function () {
        setTimeout(function () {
            if (check()) {
                bis();
            } else {
                wait();
            }
        }, 1000);
    }
    wait();

}(window);


