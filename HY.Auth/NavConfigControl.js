

!function () {
    var bis = function () {
        //return;
        Ext.onReady(function () {
            var columns = [{
                xtype: 'treecolumn',
                text: '标题',
                flex: 3,
                hideable: false,
                dataIndex: 'Title'
            }];
            var fields = [{ name: 'Title', type: 'string' }, { name: 'Url', type: 'string' }];

            Ext.define('Nav.CheckColumn', {
                extend: 'Ext.grid.column.CheckColumn',
                renderer: function (value, meta, record) {
                    var me = this;
                    if (record.data.leaf) {
                        return me.callParent(arguments);
                    } else {
                        return "";
                    }
                }
            });

            //构建列
            !function () {
                for (var i = 0; i < NavConfig.roles.length; i++) {
                    columns.push(Ext.create('Nav.CheckColumn', {
                        //xtype: 'checkcolumn',
                        text: NavConfig.roles[i],
                        flex: 1,
                        sortable: false,
                        hideable: false,
                        dataIndex: NavConfig.roles[i]
                    }));
                    fields.push({ name: NavConfig.roles[i], type: 'boolean' });
                }
            }();

            Ext.define('NavModel', {
                extend: 'Ext.data.Model',
                fields: fields
            });

            var NavStore = Ext.create('Ext.data.TreeStore', {
                //autoLoad: true,
                model: NavModel,
                proxy: {
                    type: 'ajax',
                    url: '.NavConfig'
                },
                //root: NavConfig.data,
                root: { Title: ".", leaf: false, },
                sorters: [{
                    property: 'leaf',
                    direction: 'ASC'
                }, {
                    property: 'Title',
                    direction: 'ASC'
                }]
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
                                    NavStore.reload();
                                    Ext.Msg.alert('成功', '保存成功');
                                }
                            }
                        });
                    }
                }, {
                    xtype: 'button',
                    text: '重 置',
                    handler: function () {
                        NavStore.reload();
                        return false;
                        var node = NavStore.getRootNode().childNodes[0].data
                        for (var i = 0; i < NavConfig.roles.length; i++) {
                            //node[NavConfig.roles[i]] = true;
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
        }, 200);
    }
    wait();

}(window);


