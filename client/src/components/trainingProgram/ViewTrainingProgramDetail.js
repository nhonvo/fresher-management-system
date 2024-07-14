import { Container } from '@mui/material'
import React, { useContext, useEffect } from 'react'
import { useParams } from 'react-router-dom'
import { TrainingProgramContext } from '../../context/TrainingProgramContext'
import { Card } from 'antd';
import { useState } from 'react';
import { Layout, Typography, Space, Menu, Dropdown, Tag, Button, Divider, Collapse } from 'antd';
import { EllipsisOutlined, EyeOutlined, MoreOutlined, UnorderedListOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { AuthContext } from '../../context/AuthContext';
const { Header, Content } = Layout;
const { Title, Text, Paragraph } = Typography;
const { Panel } = Collapse;
const headerStyle = {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: '16px',
    padding: '16px',
    backgroundColor: '#f5f5f5',
};
const syllabusNameStyle = {
    fontWeight: 'bold',
    fontSize: '24px',
};

const syllabusVersionStyle = {
    fontWeight: 'bold',
    fontSize: '16px',
};

const statusIndicatorStyle = {
    width: '20px',
    height: '20px',
    borderRadius: '50%',
    marginRight: '8px',
};
function ViewTrainingProgramDetail() {
    const { id } = useParams()
    const headerTitle = "Training Program";
    const {
        TrainingProgramState: { trainingProgram, trainingProgramLoading },
        getTrainingProgram
    } = useContext(TrainingProgramContext)

    // const {
    //     authState: { user },

    // } = useContext(AuthContext)

    // console.log("user", user);

    useEffect(() =>
        getTrainingProgram(id),
        []);
    console.log("trainingprogram", trainingProgram);

    const menu = (
        <Menu>
            <Menu.Item key="edit" onClick={() => { console.log("hllo") }}>Edit</Menu.Item>
            <Menu.Item key="duplicate">Duplicate</Menu.Item>
        </Menu>
    );
    return (
        <>
            <Container sx={{ marginTop: "30px", minHeight: 360 }} fixed>
                <div style={headerStyle}>
                    <div>
                        <div style={syllabusNameStyle}>Training Program</div>
                        <Title level={2}>{trainingProgram?.name}</Title>
                        <div style={syllabusVersionStyle}>Create By{headerTitle}</div>
                    </div>

                    <div style={{ display: 'flex', alignItems: 'center' }}>
                        <div style={{ flex: '1' }} />
                        <div
                            style={{
                                ...statusIndicatorStyle,
                                backgroundColor: trainingProgram?.status == 'Active' ? 'green' : 'gray',
                            }}
                        />
                        {trainingProgram?.status == 'Active' ? 'Active' : 'Inactive'}
                        <Dropdown overlay={menu} placement="bottomRight">
                            <Button type="link" icon={<MoreOutlined />} />
                        </Dropdown>
                    </div>
                </div>

                <div style={{ flex: '1', marginLeft: '10px' }}>
                    <Divider orientation="left"> <strong>Content</strong></Divider>
                </div>


                <div style={{ marginBottom: "100px" }} >
                    <Collapse>
                        {trainingProgram?.programSyllabus?.map((programSyllabus, index) => (
                            <Panel
                                key={programSyllabus?.id}
                                style={{ background: '#2D3748', borderRadius: '20px' }}
                                header={
                                    <Space style={{ fontSize: '20px', color: 'white' }}>
                                        Syllabus: {programSyllabus?.syllabus?.name}
                                    </Space>
                                }
                            >
                                <Collapse>
                                    {programSyllabus?.syllabus?.units?.map((unit, index) => (
                                        <Panel
                                            key={unit?.id}
                                            style={{ background: '#2D3748', borderRadius: '20px' }}
                                            header={
                                                <Space style={{ fontSize: '20px', color: 'white' }}>
                                                    Unit Name:  {unit?.name}
                                                </Space>
                                            }
                                        >
                                            <Collapse>
                                                {unit?.lessons?.map((lesson, index) => (
                                                    <Panel
                                                        key={lesson?.id}
                                                        style={{ background: '#2D3748', borderRadius: '20px' }}
                                                        header={
                                                            <Space style={{ fontSize: '20px', color: 'white' }}>
                                                                Lesson Name:  {lesson?.name}
                                                            </Space>
                                                        }
                                                    >
                                                        <Collapse>
                                                            {lesson?.trainingMaterials?.map((trainingMaterial, index) => (
                                                                <Panel
                                                                    key={trainingMaterial?.id}
                                                                    style={{ background: '#2D3748', borderRadius: '20px' }}
                                                                    header={
                                                                        <Space style={{ fontSize: '20px', color: 'white' }}>
                                                                            TrainingMaterial Name:  {trainingMaterial?.name}
                                                                        </Space>
                                                                    }
                                                                >
                                                                    <div style={{ display: 'flex',  alignItems: 'center', justifyContent: 'center'  }}>
                                                                        <Text strong style={{ marginRight: '5px' }}>fileName:</Text>
                                                                        <Text style={{ marginRight: '10px' }}> {trainingMaterial?.fileName}</Text>
                                                                        <Text strong style={{ marginRight: '5px' }}>filePath:</Text>
                                                                        <Text style={{ marginRight: '10px' }}>{trainingMaterial?.filePath}</Text>
                                                                        <Text strong style={{ marginRight: '5px' }}>fileSize:</Text>
                                                                        <Text style={{ marginRight: '10px' }}>{trainingMaterial?.fileSize}</Text>
                                                                    </div>
                                                                    {/* {trainingMaterial?.name} */}
                                                                </Panel>
                                                            ))}
                                                        </Collapse>
                                                    </Panel>

                                                ))}
                                            </Collapse>
                                        </Panel>
                                    ))}
                                </Collapse>
                            </Panel>
                        ))}
                    </Collapse>
                </div>
            </Container >
        </>
    )

}

export default ViewTrainingProgramDetail