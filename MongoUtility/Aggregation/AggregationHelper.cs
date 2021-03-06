﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoUtility.Core;
using MongoUtility.ToolKit;
using System.Linq;

namespace MongoUtility.Aggregation
{
    public static class AggregationHelper
    {
        public static string Distinct(string strKey, List<DataFilter.QueryConditionInputItem> distinctConditionList)
        {
            var strResult = string.Empty;
            var resultList = RuntimeMongoDbContext.GetCurrentCollection()
                        .Distinct(strKey, QueryHelper.GetQuery(distinctConditionList)).ToList();
            resultList.Sort();
            //防止错误的条件造成的海量数据
            var count = 0;
            foreach (var item in resultList)
            {
                if (count == 1000)
                {
                    strResult = "Too many result,Display first 1000 records" + Environment.NewLine + strResult;
                    break;
                }
                strResult += item.ToJson(MongoHelper.JsonWriterSettings) + Environment.NewLine;
                count++;
            }
            strResult = "Distinct Count: " + resultList.Count + Environment.NewLine + Environment.NewLine + strResult;
            return strResult;
        }

        /// <summary>
        ///     PipelineStages
        /// </summary>
        /// <returns></returns>
        public static string[] PipelineStages()
        {
            return new[]{
                "$project",
                "$match",
                "$redact",
                "$limit",
                "$skip",
                "$unwind",
                "$group",
                "$sample",
                "$sort",
                "$geoNear",
                "$lookup",
                "$out",
                "$indexStats",
                "$sortByCount",
                "$addFields"
            };
        }

        /// <summary>
        ///     Group function
        ///     https://docs.mongodb.com/master/reference/operator/aggregation-group/
        /// </summary>
        /// <returns></returns>
        public static string[] GetGroupfunction()
        {
            return new[]
            {
                "$sum",
                "$avg",
                "$first",
                "$last",
                "$max",
                "$min",
                "$push",
                "$addToSet",
                "$stdDevPop",
                "$stdDevSamp"
            };
        }

        /// <summary>
        ///     Comparison function
        ///     https://docs.mongodb.com/master/reference/operator/aggregation-comparison/
        /// </summary>
        /// <returns></returns>
        public static string[] GetComparisonfunction()
        {
            return new[]
            {
                "$cmp",
                "$eq",
                "$gt",
                "$gte",
                "$lt",
                "$lte",
                "$ne"
            };
        }
    }
}