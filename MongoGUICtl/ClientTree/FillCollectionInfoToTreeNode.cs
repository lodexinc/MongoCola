﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoUtility.Basic;
using MongoUtility.Core;
using ResourceLib.Method;

namespace MongoGUICtl.ClientTree
{
    public static partial class UiHelper
    {
        /// <summary>
        ///     View处理
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static TreeNode FillViewInfoToTreeNode(IMongoCollection<BsonDocument> col, string TagPrefix)
        {
            var mongoColNode = new TreeNode("Views(" + col.Count(x => true).ToString() + ")");
            mongoColNode.ImageIndex = (int)GetSystemIcon.MainTreeImageType.CollectionList;
            mongoColNode.SelectedImageIndex = (int)GetSystemIcon.MainTreeImageType.CollectionList;
            var r = col.Find(x => true);

            foreach (var viewDoc in r.ToList())
            {
                var id = viewDoc.GetElement(ConstMgr.KeyId).ToString();
                var viewNode = new TreeNode(id.Substring(id.IndexOf(".") + 1));
                viewNode.ImageIndex = (int)GetSystemIcon.MainTreeImageType.Collection;
                viewNode.SelectedImageIndex = (int)GetSystemIcon.MainTreeImageType.Collection;

                //ViewOn
                var viewOnNode = new TreeNode("ViewOn:" + viewDoc.GetElement("viewOn").Value.ToString());
                viewOnNode.ImageIndex = (int)GetSystemIcon.MainTreeImageType.KeyInfo;
                viewOnNode.SelectedImageIndex = (int)GetSystemIcon.MainTreeImageType.KeyInfo;
                viewNode.Nodes.Add(viewOnNode);
                //Pipeline
                var pipelineNode = new TreeNode("pipeline:" + viewDoc.GetElement("pipeline").Value.ToString());
                pipelineNode.ImageIndex = (int)GetSystemIcon.MainTreeImageType.KeyInfo;
                pipelineNode.SelectedImageIndex = (int)GetSystemIcon.MainTreeImageType.KeyInfo;
                viewNode.Nodes.Add(pipelineNode);
                viewNode.Tag = TagPrefix + viewNode.Text;
                mongoColNode.Nodes.Add(viewNode);
            }
            return mongoColNode;
        }

        /// <summary>
        ///     将数据集放入Node
        /// </summary>
        /// <param name="col"></param>
        /// <param name="mongoConnSvrKey"></param>
        /// <returns></returns>
        public static TreeNode FillCollectionInfoToTreeNode(IMongoCollection<BsonDocument> col, string mongoConnSvrKey)
        {
            var colName = col.CollectionNamespace.CollectionName;
            var databaseName = col.CollectionNamespace.DatabaseNamespace.DatabaseName;
            var strShowColName = GetShowName(databaseName, colName);
            //Collection件数的表示
            long colCount = 0;
            Expression<Func<BsonDocument, bool>> countfun = x => true;
            var task = Task.Run(
                async () => { colCount = await col.CountAsync(countfun); }
                );
            task.Wait();
            strShowColName = strShowColName + "(" + colCount + ")";
            var mongoColNode = new TreeNode(strShowColName);
            switch (col.CollectionNamespace.CollectionName)
            {
                case ConstMgr.CollectionNameGfsFiles:
                    mongoColNode.Tag = ConstMgr.GridFileSystemTag + ":" + mongoConnSvrKey + "/" + databaseName + "/" +
                                       col.CollectionNamespace.CollectionName;
                    break;
                case ConstMgr.CollectionNameUser:
                    mongoColNode.Tag = ConstMgr.UserListTag + ":" + mongoConnSvrKey + "/" + databaseName + "/" +
                                       col.CollectionNamespace.CollectionName;
                    break;
                default:
                    mongoColNode.Tag = TagInfo.CreateTagInfo(mongoConnSvrKey, databaseName,
                        col.CollectionNamespace.CollectionName);
                    break;
            }

            try
            {
                //View 没有Index
                IAsyncCursor<BsonDocument> indexCursor = null;
                task = Task.Run(
                    async () => { indexCursor = await col.Indexes.ListAsync(); }
                    );
                task.Wait();
                List<BsonDocument> indexDocs = null;
                task = Task.Run(
                    async () => { indexDocs = await indexCursor.ToListAsync(); }
                    );
                task.Wait();
                foreach (var indexDoc in indexDocs)
                {
                    var mongoIndexes = new TreeNode { Text = indexDoc.GetElement("name").Value.ToString() };
                    foreach (var item in indexDoc.Elements)
                    {
                        mongoIndexes.Nodes.Add(string.Empty, item.Name + ":" + item.Value,
                            (int)GetSystemIcon.MainTreeImageType.KeyInfo,
                            (int)GetSystemIcon.MainTreeImageType.KeyInfo);
                    }
                    mongoIndexes.ImageIndex = (int)GetSystemIcon.MainTreeImageType.Keys;
                    mongoIndexes.SelectedImageIndex = (int)GetSystemIcon.MainTreeImageType.Keys;
                    mongoIndexes.Tag = ConstMgr.IndexesTag + ":" + mongoConnSvrKey + "/" + databaseName + "/" +
                                       col.CollectionNamespace.CollectionName;
                    mongoColNode.Nodes.Add(mongoIndexes);
                }
            }
            catch (Exception)
            {
                return null;
            }

            if (col.CollectionNamespace.CollectionName == ConstMgr.CollectionNameUser)
            {
                mongoColNode.ImageIndex = (int)GetSystemIcon.MainTreeImageType.UserIcon;
                mongoColNode.SelectedImageIndex = (int)GetSystemIcon.MainTreeImageType.UserIcon;
            }
            else
            {
                mongoColNode.ImageIndex = (int)GetSystemIcon.MainTreeImageType.Collection;
                mongoColNode.SelectedImageIndex = (int)GetSystemIcon.MainTreeImageType.Collection;
            }
            //End Data
            return mongoColNode;
        }


        public static string GetShowName(string databaseName, string CollectionName)
        {
            var strShowColName = CollectionName;
            if (!GuiConfig.IsUseDefaultLanguage)
            {
                switch (databaseName)
                {
                    case "config":
                        switch (strShowColName)
                        {
                            case "actionlog":
                                strShowColName = GuiConfig.GetText("actionlog") + "(" + strShowColName + ")";
                                break;
                            case "chunks":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameChunks) + "(" + strShowColName + ")";
                                break;
                            case "collections":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameCollections) + "(" + strShowColName + ")";
                                break;
                            case "changelog":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameChangelog) + "(" + strShowColName + ")";
                                break;
                            case "databases":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameDatabases) + "(" + strShowColName + ")";
                                break;
                            case "lockpings":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameLockpings) + "(" + strShowColName + ")";
                                break;
                            case "locks":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameLocks) + "(" + strShowColName + ")";
                                break;
                            case "migrations":
                                strShowColName = GuiConfig.GetText("migrations") + "(" + strShowColName + ")";
                                break;
                            case "mongos":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameMongos) + "(" + strShowColName + ")";
                                break;
                            case "settings":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameSettings) + "(" + strShowColName + ")";
                                break;
                            case "shards":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameShards) + "(" + strShowColName + ")";
                                break;
                            case "tags":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameTags) + "(" + strShowColName + ")";
                                break;
                            case "version":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameVersion) + "(" + strShowColName + ")";
                                break;
                            default:
                                break;
                        }
                        break;
                    case "local":
                        switch (strShowColName)
                        {
                            case "me":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameMe) + "(" + strShowColName + ")";
                                break;
                            case "sources":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameSources) + "(" + strShowColName + ")";
                                break;
                            case "slaves":
                                strShowColName = GuiConfig.GetText(TextType.SystemcColnameSlaves) + "(" + strShowColName + ")";
                                break;
                            case "startup_log":
                                strShowColName = GuiConfig.GetText("startup_log") + "(" + strShowColName + ")";
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

            switch (strShowColName)
            {
                case ConstMgr.CollectionNameOperationLog:
                    strShowColName =
                        GuiConfig.GetText(
                            TextType.CollectionNameOperationLog) +
                        "(" + strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameGfsChunks:
                    strShowColName =
                        GuiConfig.GetText(
                            TextType.CollectionNameGfsChunks) +
                        "(" + strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameGfsFiles:
                    strShowColName =
                        GuiConfig.GetText(
                            TextType.CollectionNameGfsFiles) +
                        "(" + strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameSystemIndexes:
                    strShowColName =
                        GuiConfig.GetText(
                            TextType.CollectionNameSystemIndexes) +
                        "(" + strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameJavascript:
                    strShowColName =
                        GuiConfig.GetText(
                            TextType.CollectionNameJavascript) +
                        "(" + strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameSystemReplset:
                    strShowColName =
                        GuiConfig.GetText(
                            TextType.CollectionNameSystemReplset) +
                        "(" + strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameReplsetMinvalid:
                    strShowColName =
                        GuiConfig.GetText(
                            TextType.CollectionNameReplsetMinvalid) + "(" + strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameUser:
                    strShowColName =
                        GuiConfig.GetText(TextType.CollectionNameUser) +
                        "(" +
                        strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameRole:
                    //New From 2.6 
                    strShowColName =
                        GuiConfig.GetText(TextType.CollectionNameRole) +
                        "(" +
                        strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameSystemProfile:
                    strShowColName =
                        GuiConfig.GetText(
                            TextType.CollectionNameSystemProfile) +
                        "(" + strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameVersion:
                    strShowColName =
                        GuiConfig.GetText("version") +
                        "(" + strShowColName + ")";
                    break;
                case ConstMgr.CollectionNameReplsetElection:
                    strShowColName = GuiConfig.GetText("election") + "(" + strShowColName + ")";
                    break;
                default:
                    break;
            }

            return strShowColName;
        }

        /// <summary>
        ///     将数据集放入Node
        /// </summary>
        /// <param name="jsNode"></param>
        /// <param name="col"></param>
        /// <param name="mongoConnSvrKey"></param>
        /// <param name="strDbName"></param>
        /// <returns></returns>
        public static void FillJavaScriptInfoToTreeNode(TreeNode jsNode, IMongoCollection<BsonDocument> col,
            string mongoConnSvrKey, string strDbName)
        {
            var tag = ConstMgr.JavascriptDocTag + ":" + mongoConnSvrKey + "/" + strDbName + "/" +
                      col.CollectionNamespace.CollectionName;
            var server = RuntimeMongoDbContext.GetMongoServerBySvrPath(tag, RuntimeMongoDbContext.MongoConnSvrLst);
            var db = RuntimeMongoDbContext.GetMongoDBBySvrPath(tag, server);
            MongoCollection mongoJsCol = db.GetCollection(ConstMgr.CollectionNameJavascript);
            var list = mongoJsCol.FindAllAs<BsonDocument>()
                .Select(item => item.GetValue(ConstMgr.KeyId).ToString())
                .OrderBy(item => item)
                .ToList();

            foreach (var name in list)
            {
                var node = new TreeNode(name)
                {
                    ImageIndex = (int)GetSystemIcon.MainTreeImageType.JsDoc,
                    SelectedImageIndex = (int)GetSystemIcon.MainTreeImageType.JsDoc,
                    Tag = tag + "/" + name
                };
                jsNode.Nodes.Add(node);
            }

            jsNode.ImageIndex = (int)GetSystemIcon.MainTreeImageType.JavaScriptList;
            jsNode.SelectedImageIndex = (int)GetSystemIcon.MainTreeImageType.JavaScriptList;
        }
    }
}