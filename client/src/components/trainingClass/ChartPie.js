import React from "react";
import { G2 } from '@ant-design/plots';
import { Pie } from '@ant-design/plots';

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

const ChartPie = ({ data }) => {
  console.log("Char data:", data)
  const cfg = {
    appendPadding: 10,
    data,
    angleField: 'value',
    colorField: 'type',
    radius: 0.75,
    legend: false,
    label: {
      type: 'spider',
      labelHeight: 40,
      formatter: (data, mappingData) => {
        const group = new G.Group({});
        group.addShape({
          type: 'circle',
          attrs: {
            x: 0,
            y: 0,
            width: 40,
            height: 50,
            r: 5,
            fill: mappingData.color,
          },
        });
        group.addShape({
          type: 'text',
          attrs: {
            x: 10,
            y: 8,
            text: `${data.type}`,
            fill: mappingData.color,
          },
        });
        group.addShape({
          type: 'text',
          attrs: {
            x: 0,
            y: 25,
            text: `${data.value}%`,
            fill: 'rgba(0, 0, 0, 0.65)',
            fontWeight: 700,
          },
        });
        return group;
      },
    },
    interactions: [
      {
        type: 'element-selected',
      },
      {
        type: 'element-active',
      },
    ],
  };
  return <Pie {...cfg} />;
  // Logic để vẽ biểu đồ pie ở đây
};

export default ChartPie;