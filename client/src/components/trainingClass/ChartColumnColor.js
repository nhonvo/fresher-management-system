import React from "react";
import { G2 } from '@ant-design/plots';
import { Column } from '@ant-design/plots';

const G = G2.getEngine('canvas');
const data = [
  {
    type: 'Quiz',
    value: 40,
  },
  {
    type: 'Assignment',
    value: 30,
  },
  {
    type: 'Final',
    value: 30,
  },
];

const ChartColumnColor = ({ data }) => {
  console.log("Char data:", data)
  const config = {
    data,
    isStack: true,
    xField: 'year',
    yField: 'value',
    seriesField: 'type',
    label: {
      // 可手动配置 label 数据标签位置
      position: 'middle', // 'top', 'bottom', 'middle'
    },
    interactions: [
      {
        type: 'active-region',
        enable: false,
      },
    ],
    connectedArea: {
      style: (oldStyle, element) => {
        return {
          fill: 'rgba(0,0,0,0.25)',
          stroke: oldStyle.fill,
          ColumnWidth: 0.5,
        };
      },
    },
  };

  return <Column {...config} />;
  // Logic để vẽ biểu đồ Column ở đây
};

export default ChartColumnColor;