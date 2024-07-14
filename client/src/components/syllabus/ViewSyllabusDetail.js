import React, { useContext, useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import { Container } from '@mui/material'
import { SyllabusContext } from '../../context/SyllabusContext';
import { Skeleton, Tabs, Spin } from 'antd';
import { Card, Space } from 'antd';
import { Col, Divider, Row, Typography, Button, Menu, Dropdown, Collapse, Upload } from 'antd';
import { StarOutlined, UsergroupDeleteOutlined, SecurityScanOutlined, MoreOutlined } from '@ant-design/icons';
import { Input, Modal, Form, Select, Icon, InputNumber, message, List } from 'antd';
import { EditOutlined, SaveOutlined, CloseOutlined, PlusOutlined, UnorderedListOutlined, DeleteOutlined, UploadOutlined, DownloadOutlined, ShareAltOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { G2 } from '@ant-design/plots';
import { Pie } from '@ant-design/plots';
import axios from 'axios';
import { apiUrl } from '../../context/Constants';
const { Title } = Typography;
const { Panel } = Collapse;
const { Option } = Select
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

const HandleOnClickEdit = () => {
    console.log("Hello");
}
const menu = (
    <Menu>
        <Menu.Item key="edit" onClick={HandleOnClickEdit}>Edit</Menu.Item>
        <Menu.Item key="duplicate">Duplicate</Menu.Item>
    </Menu>
);
// const menu = (
//     <Menu items={[
//       { key: 'edit', children: 'Edit' },
//       { key: 'duplicate', children: 'Duplicate' },
//     ]} />
//   );



const ViewSyllabusDetail = () => {

    const [isEvent, setIsEvent] = useState(false);
    const [isEditMode, setIsEditMode] = useState(false);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [isModalVisibleDelete, setIsModalVisibleDelete] = useState(false);
    const navigate = useNavigate();
    const [editMode, setEditMode] = useState(false);
    const [sections, setSections] = useState([]);

    const [modalVisibleAddSection, setModalVisibleAddSection] = useState(false);
    const [modalVisibleAddLesson, setModalVisibleAddLesson] = useState(false);
    const [modalVisibleAddMaterial, setModalVisibleAddMaterial] = useState(false);


    const [editingSection, setEditingSection] = useState(null);

    const [stateLesson, setStateLesson] = useState(false)
    const [stateMaterial, setStateMaterial] = useState(false)



    const [editingLesson, setEditingLesson] = useState(null);
    const [editingSaveSectionLesson, setEditingSaveSectionLesson] = useState(null);

    const [editingMaterial, setEditingMaterial] = useState(null);
    const [editingSaveLessonMaterial, setEditingSaveLessonMaterial] = useState(null);



    const [modalDeleteUnit, setModalDeleteUnit] = useState(false);
    const [modalDeleteLesson, setModalDeleteLesson] = useState(false);
    const [modalDeleteMaterial, setModalDeleteMaterial] = useState(false);

    const [file, setFile] = useState(null);
    const [visibleModalMaterial, setVisibleModalMaterial] = useState(false);

    const {
        syllabusState: { syllabus, syllabusLoading, syllabusDetail },
        getSyllabus,
        setSyllabusState,
        getSyllabusDetail,
        updateSyllabus,
        DeleteSyllabus,
        AddSyllabusUnit,
        UpdateSyllabusUnit,
        DeleteSyllabusUnit,
        AddSyllabusLesson,
        UpdateSyllabusLesson,
        DeleteSyllabusLesson,
        AddSyllabusMaterial
    } = useContext(SyllabusContext)

    const { id } = useParams()

    const [form] = Form.useForm();
    const [formUnit] = Form.useForm();
    const [fromLesson] = Form.useForm();
    const [fromMaterial] = Form.useForm();



    const handleSave = () => {
        form
            .validateFields()
            .then(async (values) => {
                values.id = syllabusDetail.id;
                await updateSyllabus(values)
                getSyllabusDetail(id)
                setIsEvent(true)
            })
            .catch((error) => {
                console.log(error);
            });
        setIsModalVisible(!isModalVisible)
    };
    const handleEditClick = () => {
        setIsModalVisible(true);

    };

    const handleDeleteOkClick = () => {
        DeleteSyllabus(id)
        getSyllabusDetail(id)
        setIsModalVisibleDelete(!isModalVisibleDelete);
        navigate('/ViewSyllabus');
    };
    const handleDeleteClick = () => {
        setIsModalVisibleDelete(true);

    }

    // console.log(syllabus);
    // console.log(isModalVisible);


    const generalChildren = (se) => {
        return (
            <Space direction="vertical" size="middle" style={{ display: 'flex' }}>
                <Row
                    gutter={{
                        xs: 8,
                        sm: 16,
                        md: 24,
                        lg: 32,
                    }}
                >
                    <Col className="gutter-row" span={7}>
                        <Card size="small" >
                            <Row align="middle">
                                <Col flex="auto">
                                    <div style={{ display: 'flex', alignItems: 'center' }}>
                                        <StarOutlined style={{ marginRight: '8px' }} />
                                        <strong style={{ lineHeight: '1', marginBottom: '0' }}>Level:</strong>
                                    </div>
                                </Col>
                                <Col flex="auto" style={{ textAlign: 'left' }}>
                                    <p style={{ margin: 0 }}>{syllabusDetail?.syllabusLevel}</p>
                                </Col>
                            </Row>

                            <Row align="middle">
                                <Col flex="auto">
                                    <div style={{ display: 'flex', alignItems: 'center' }}>
                                        <UsergroupDeleteOutlined style={{ marginRight: '8px' }} />
                                        <strong style={{ lineHeight: '1', marginBottom: '0' }}>Attendee Number:</strong>
                                    </div>
                                </Col>
                                <Col flex="auto" style={{ textAlign: 'left' }}>
                                    <p style={{ margin: 0 }}>{syllabusDetail?.attendeeNumber}</p>
                                </Col>
                            </Row>
                        </Card>
                    </Col>

                    <Col className="gutter-row" span={17}>
                        <Card title="Course Objective" size="small">
                            <p>{syllabusDetail?.courseObjective}</p>
                        </Card>
                    </Col>
                </Row>

                <Col className="gutter-row" span={24}>
                    <Card title="Evaluation Core" size="small">
                        <Row align="middle">
                            <Col flex="auto">
                                <div style={{ display: 'flex', alignItems: 'center' }}>
                                    <StarOutlined style={{ marginRight: '8px' }} />
                                    <strong style={{ lineHeight: '1', marginBottom: '0' }}>Quiz Scheme: {syllabusDetail?.quizScheme * 100}%</strong>
                                </div>
                                <div style={{ display: 'flex', marginTop: "15px", alignItems: 'center' }}>
                                    <StarOutlined style={{ marginRight: '8px' }} />
                                    <strong style={{ lineHeight: '1', marginBottom: '0' }}>Assignment Scheme: {syllabusDetail?.assignmentScheme * 100}%</strong>
                                </div>
                                <div style={{ display: 'flex', marginTop: "15px", alignItems: 'center' }}>
                                    <StarOutlined style={{ marginRight: '8px' }} />
                                    <strong style={{ lineHeight: '1', marginBottom: '0' }}>GPA Scheme: {syllabusDetail?.gpaScheme * 100}%</strong>
                                </div>
                            </Col>
                            <Col flex="auto" style={{ alignContent: "space-between", textAlign: 'center' }}>
                                <div style={{ display: 'flex', alignItems: 'center' }}>
                                    <StarOutlined style={{ marginRight: '8px' }} />
                                    <strong style={{ lineHeight: '1', marginBottom: '0' }}>Final Criteria: {syllabusDetail?.assignmentScheme * 100}%</strong>
                                </div>
                                <div style={{ display: 'flex', marginTop: "15px", alignItems: 'center' }}>
                                    <StarOutlined style={{ marginRight: '8px' }} />
                                    <strong style={{ lineHeight: '1', marginBottom: '0' }}>Final Practical Criteria: {syllabusDetail?.finalPracticeScheme * 100}%</strong>
                                </div>
                                <div style={{ display: 'flex', marginTop: "15px", alignItems: 'center' }}>
                                    <StarOutlined style={{ marginRight: '8px' }} />
                                    <strong style={{ lineHeight: '1', marginBottom: '0' }}>Final Theory Criteria: {syllabusDetail?.finalTheoryScheme * 100}%</strong>
                                </div>
                            </Col>
                        </Row>
                    </Card>
                </Col>
                <Row justify="end" style={{ marginTop: '16px' }}>
                    <Col>
                        <Button type="primary" icon={<EditOutlined />}
                            onClick={handleEditClick}
                            style={{ backgroundColor: 'green', color: 'white' }}>
                            Edit
                        </Button>
                    </Col>
                    <Col>
                        <Button
                            type="default"
                            icon={<EditOutlined />}
                            onClick={handleDeleteClick}
                            style={{ marginLeft: '10px', background: 'red', color: '#888888' }}
                        >
                            Delete
                        </Button>
                    </Col>
                </Row>


                <Modal
                    visible={isModalVisible} onCancel={() => setIsModalVisible(false)}
                    title="Edit Syllabus"
                    onOk={handleSave}
                // Các thuộc tính khác của Modal
                >
                    <Form form={form} initialValues={syllabusDetail}>
                        <Form.Item label="Name" name="name" rules={[{ required: true }]}>
                            <Input />
                        </Form.Item>

                        <Form.Item label="Code" name="code" rules={[{ required: true }]}>
                            <Input />
                        </Form.Item>


                        <Form.Item
                            label="Attendee Number"
                            name="attendeeNumber"
                            rules={[{ required: true }]}
                        >
                            <Input type="number" />
                        </Form.Item>
                        <Form.Item label="Final Criteria" name="finalScheme" rules={[{ required: true }]}>
                            <Input type="number" />
                        </Form.Item>
                        <Form.Item label="Final Practical Criteria" name="finalPracticeScheme" rules={[{ required: true }]}>
                            <Input type="number" />
                        </Form.Item>
                        <Form.Item label="Final Theory Criteria" name="finalTheoryScheme" rules={[{ required: true }]}>
                            <Input type="number" />
                        </Form.Item>
                        <Form.Item label="Quiz Scheme" name="quizScheme" rules={[{ required: true }]}>
                            <Input type="number" />
                        </Form.Item>
                        <Form.Item label="GPA Scheme" name="gpaScheme" rules={[{ required: true }]}>
                            <Input type="number" />
                        </Form.Item>
                        <Form.Item label="Assignment Scheme" name="assignmentScheme" rules={[{ required: true }]}>
                            <Input type="number" />
                        </Form.Item>
                        <Form.Item label="Course Objective" name="courseObjective">
                            <Input.TextArea rows={4} />
                        </Form.Item>
                        <Form.Item label="Syllabus Level" name="syllabusLevel">
                            <Select>
                                <Option value="Beginner">Beginner</Option>
                                <Option value="Intermediate">Intermediate</Option>
                                <Option value="AllLevel">All Level</Option>
                                <Option value="Advance">Advance</Option>
                            </Select>
                        </Form.Item>
                    </Form>
                </Modal>
                <Modal
                    title="Xác thực xóa"
                    visible={isModalVisibleDelete}
                    onOk={handleDeleteOkClick}
                    onCancel={() => setIsModalVisibleDelete(!isModalVisibleDelete)}
                >
                    <p>Bạn có chắc chắn muốn xóa không?</p>
                </Modal>
                <Modal
                    title="Xác thực xóa"
                    visible={isModalVisibleDelete}
                    onOk={handleDeleteOkClick}
                    onCancel={() => setIsModalVisibleDelete(!isModalVisibleDelete)}
                >
                    <p>Bạn có chắc chắn muốn xóa không?</p>
                </Modal>
            </Space>
        )
    }


    //handle 
    //units
    const handleAddOkSection = () => {
        formUnit.validateFields().then(async values => {
            values.syllabusId = id
            await AddSyllabusUnit(values, id)
            getSyllabusDetail(id)

            formUnit.resetFields();
            console.log(values);
            setModalVisibleAddSection(false);
        })

    };
    const handleEditSection = () => {
        formUnit.validateFields().then(values => {
            values.syllabusId = id
            values.id = editingSection.id
            UpdateSyllabusUnit(values)
            getSyllabusDetail(id)

            setModalVisibleAddSection(false);
            formUnit.resetFields();
            setEditingSection(null);
        });
    };

    const handleCancelAddUnit = () => {
        setModalVisibleAddSection(false);
        getSyllabusDetail(id)

        formUnit.resetFields();
        setEditingSection(null);
    };

    const handleDeleteUnitOkClick = () => {
        getSyllabusDetail(id)
        setModalDeleteUnit(!modalDeleteUnit)
        DeleteSyllabusUnit(editingSection.id)
    }
    //lesson
    const handleCancelAddLesson = () => {
        getSyllabusDetail(id)
        setModalVisibleAddLesson(false);
        fromLesson.resetFields();
        setStateLesson(false);
    };

    const handleAddOkLesson = () => {
        fromLesson.validateFields().then(values => {
            values.unitId = editingSaveSectionLesson.id;
            console.log(values);
            AddSyllabusLesson(values, id);
            getSyllabusDetail(id)
            setModalVisibleAddLesson(false);
            fromLesson.resetFields();
        });
    };

    const handleEditLesson = () => {
        fromLesson.validateFields().then(values => {
            values.unitId = editingSaveSectionLesson.id;
            values.id = editingLesson.id
            UpdateSyllabusLesson(values);
            getSyllabusDetail(id)

            setModalVisibleAddLesson(false);
            fromLesson.resetFields();
            setEditingSaveSectionLesson(null);
        });
    };
    const handleDeleteLessonOkClick = () => {
        DeleteSyllabusLesson(editingLesson.id)
        getSyllabusDetail(id)
        setModalDeleteLesson(!modalDeleteLesson)

    }


    // Material
    const handleCancelAddMaterial = () => {
        setModalVisibleAddMaterial(false);
        fromMaterial.resetFields();
        stateMaterial(false);
    };

    const handleAddOkMaterial = () => {
        fromMaterial.validateFields().then(values => {
            values.unitLessonId = editingSaveLessonMaterial.id;
            AddSyllabusMaterial(values, id);
            getSyllabusDetail(id)
            setModalVisibleAddMaterial(false);
            fromLesson.resetFields();
        });
        console.log("handleAddOkMaterial");
    };
    const handleUpload = async () => {
        let fileUpload = file
        const formData = new FormData();
        formData.append('TrainingMaterials', fileUpload)
        await AddSyllabusMaterial(formData, editingSaveLessonMaterial.id)
        setVisibleModalMaterial(!visibleModalMaterial)

    }
    const handleMaterialAddCancel = () => {
        setVisibleModalMaterial(false);
    };

    const handleFileChange = (e) => {
        let file = e.target.files[0];
        setFile(file);

    };
    const handleEditMaterial = () => {
        // fromLesson.validateFields().then(values => {
        //     values.unitId = editingSaveSectionLesson.id;
        //     values.id = editingLesson.id
        //     UpdateSyllabusLesson(values);
        //     setModalVisibleAddLesson(false);
        //     fromLesson.resetFields();
        //     setEditingSaveSectionLesson(null);
        // });
        console.log("handleEditMaterial");
    };
    const handleDeleteMaterialOkClick = () => {
        // setModalDeleteLesson(!modalDeleteLesson)
        // DeleteSyllabusLesson(editingLesson.id)
        console.log("handleDeleteMaterialOkClick");
    }

    const handleClickOpenDownload = (materialId) => {
        const url = `${apiUrl}TrainingMaterial/${materialId}/download`;
        window.open(url, '_blank'); // Mở liên kết trong cửa sổ/tab mới
    };
    const handleClickOpenShareQR = (materialId) => {
        window.open(`https://qr.softvn.com?q=trainingmanagementsystem.azurewebsites.net/api/TrainingMaterial/${materialId}/download`);
    }
    const test = () => {
        return (
            <>
                <Collapse>
                    {syllabusDetail?.units?.map((section, index) => (
                        <Panel
                            showArrow={false}
                            key={section.id}
                            header={
                                <Space style={{ fontSize: '20px', color: 'white' }}>
                                    <UnorderedListOutlined style={{ color: 'white' }} />
                                    <strong> Section {index + 1}:  {section.name}</strong>
                                </Space>
                            }
                            style={{ background: '#2D3748', borderRadius: '20px' }}
                            extra={[
                                <EditOutlined key="edit" onClick={() => {
                                    setModalVisibleAddSection(!modalVisibleAddSection)
                                    setEditingSection(section);
                                    formUnit.setFieldsValue({
                                        name: section.name,
                                        syllabusSession: section.syllabusSession,
                                        unitNumber: section.unitNumber,
                                        syllabusId: section.syllabusId,
                                        id: section.id
                                    });
                                }} />,
                                <DeleteOutlined key="delete" onClick={() => {
                                    setEditingSection(section);
                                    setModalDeleteUnit(!modalDeleteUnit)
                                }} />,
                            ]}
                        >
                            <Collapse>
                                {section?.lessons?.map(lesson => (
                                    <Panel
                                        showArrow={false}
                                        key={lesson.id}
                                        style={{ background: 'PaleGoldenRod', borderRadius: '20px' }}
                                        header={
                                            <Space>
                                                <UnorderedListOutlined />
                                                <strong>
                                                    Lesson:  {lesson.name}
                                                </strong>
                                            </Space>
                                        }
                                        extra={[
                                            <EditOutlined key="edit" onClick={() => {
                                                setStateLesson(true)
                                                setEditingSaveSectionLesson(section)
                                                setEditingLesson(lesson)
                                                setModalVisibleAddLesson(!modalVisibleAddLesson)
                                                fromLesson.setFieldsValue({
                                                    name: lesson.name,
                                                    duration: lesson.duration,
                                                    lessonType: lesson.lessonType,
                                                    deliveryType: lesson.deliveryType
                                                })

                                            }} />,
                                            <DeleteOutlined key="delete" onClick={() => {

                                                setEditingLesson(lesson);
                                                setModalDeleteLesson(!modalDeleteLesson)
                                                // setEditingSection(section);
                                                // setModalDeleteUnit(!modalDeleteUnit)
                                                // console.log("delete lesson");
                                            }} />,
                                        ]}
                                    >
                                        {console.log("lesson", lesson)}
                                        <List
                                            dataSource={lesson?.trainingMaterials}
                                            renderItem={(file) => {
                                                console.log(file);
                                                return (
                                                    <List.Item>
                                                        <List.Item.Meta
                                                            title={file.fileName}
                                                            description={file.filePath}
                                                        />
                                                        <Button
                                                            onClick={() => handleClickOpenDownload(file.id)}
                                                            icon={<DownloadOutlined />}
                                                        >DownLoad</Button>
                                                        <Button
                                                            style={{ marginLeft: '10px' }}
                                                            onClick={() => handleClickOpenShareQR(file.id)}
                                                            icon={<ShareAltOutlined />}
                                                        >Share QR</Button>
                                                    </List.Item>
                                                )

                                            }


                                            }
                                        />
                                        {/* <Collapse>
                                            <Upload
                                                // {...props}
                                                onClick={() =>{
                                                    window.open(`/TrainingMaterial/${file.uid}/download`)
                                                }}
                                                fileList={lesson?.trainingMaterials?.map((fileTest, index) => {
                                                    console.log("alo", fileTest)
                                                    return {
                                                        uid: fileTest?.id,
                                                        name: fileTest?.fileName,
                                                        status:
                                                        
                                                            "done",
                                                        // url:
                                                        //     "/TrainingMaterial/" +
                                                        //     fileTest?.id,
                                                    };

                                                }


                                                )}></Upload>
                                        </Collapse> */}
                                        <Button type="primary" style={{ backgroundColor: '#85DE77', color: 'white', marginTop: '20px' }} onClick={() => {
                                            setEditingSaveLessonMaterial(lesson)
                                            setVisibleModalMaterial(!visibleModalMaterial)

                                        }}>
                                            Add Materials
                                        </Button>
                                    </Panel>

                                ))}
                            </Collapse>
                            <Button type="primary" style={{ backgroundColor: '#85DE77', color: 'white', marginTop: '20px' }} onClick={() => {
                                // console.log("section", section);
                                setEditingSaveSectionLesson(section)
                                setModalVisibleAddLesson(!visibleModalMaterial)
                            }}>
                                Add Lesson
                            </Button>
                        </Panel>
                    ))}
                </Collapse>
                <Button type="primary" style={{ backgroundColor: '#85DE77', color: 'white', marginTop: '20px' }} onClick={() => setModalVisibleAddSection(!modalVisibleAddSection)}>
                    Add Section
                </Button>
                {/* unit */}
                <Modal
                    title={editingSection ? "Edit Section" : "Add Section"}
                    visible={modalVisibleAddSection}
                    onOk={editingSection ? handleEditSection : handleAddOkSection}
                    onCancel={handleCancelAddUnit}
                >
                    <Form form={formUnit} layout="vertical">
                        <Form.Item
                            label="Name"
                            name="name"
                            rules={[{ required: true, message: 'Please enter the name' }]}
                        >
                            <Input />
                        </Form.Item>
                        <Form.Item
                            label="Syllabus Session"
                            name="syllabusSession"
                            rules={[{ required: true, message: 'Please enter the syllabus session' }]}
                        >
                            <InputNumber min={0} />
                        </Form.Item>
                        <Form.Item
                            label="Unit Number"
                            name="unitNumber"
                            rules={[{ required: true, message: 'Please enter the unit number' }]}
                        >
                            <InputNumber min={0} />
                        </Form.Item>

                    </Form>
                </Modal>
                <Modal
                    title="Xác thực xóa"
                    visible={modalDeleteUnit}
                    onOk={handleDeleteUnitOkClick}
                    onCancel={() => setModalDeleteUnit(!modalDeleteUnit)}
                >
                    <p>Bạn có chắc chắn muốn xóa không?</p>
                </Modal>
                {/* lesson */}
                <Modal
                    title={stateLesson ? "Edit Lesson" : "Add Lesson"}
                    visible={modalVisibleAddLesson}
                    onOk={stateLesson ? handleEditLesson : handleAddOkLesson}
                    onCancel={handleCancelAddLesson}
                >
                    <Form form={fromLesson} layout="vertical">
                        <Form.Item
                            label="Name"
                            name="name"
                            rules={[{ required: true, message: 'Please enter the name' }]}
                        >
                            <Input />
                        </Form.Item>
                        <Form.Item
                            label="Duration (hours)"
                            name="duration"
                            rules={[{ required: true, message: 'Please enter the duration' }]}
                        >
                            <InputNumber min={0} />
                        </Form.Item>
                        <Form.Item
                            label="Lesson Type"
                            name="lessonType"
                            rules={[{ required: true, message: 'Please enter the lesson Type' }]}
                        >
                            <Select>
                                <Select.Option style={{ backgroundColor: '#85DE77' }} value="Online">Online</Select.Option>
                                <Select.Option style={{ backgroundColor: 'orange', marginTop: '5px' }} value="Offline">Offline</Select.Option>
                            </Select>
                        </Form.Item>
                        <Form.Item
                            label="Delivery Type"
                            name="deliveryType"
                            rules={[{ required: true, message: 'Please enter the delivery Type' }]}
                        >
                            <Select>
                                <Select.Option value="AssignmentLab">AssignmentLab</Select.Option>
                                <Select.Option style={{ marginTop: '5px' }} value="ConceptLecture">ConceptLecture</Select.Option>
                                <Select.Option style={{ marginTop: '5px' }} value="GuideReview">GuideReview</Select.Option>
                                <Select.Option style={{ marginTop: '5px' }} value="TestQuiz">TestQuiz</Select.Option>
                                <Select.Option style={{ marginTop: '5px' }} value="Exam">Exam</Select.Option>
                                <Select.Option style={{ marginTop: '5px' }} value="SeminarWorkshop">SeminarWorkshop</Select.Option>
                            </Select>
                        </Form.Item>

                    </Form>
                </Modal>
                <Modal
                    title="Xác thực xóa"
                    visible={modalDeleteLesson}
                    onOk={handleDeleteLessonOkClick}
                    onCancel={() => setModalDeleteLesson(!modalDeleteLesson)}
                >
                    <p>Bạn có chắc chắn muốn xóa không?</p>
                </Modal>
                {/* material */}

                <Modal
                    title="Tải lên tập tin"
                    visible={visibleModalMaterial}
                    onOk={handleUpload}
                    onCancel={handleMaterialAddCancel}
                >
                    <input type='file' name='file' onChange={(e) => { handleFileChange(e) }} />

                </Modal>
            </>
        )
    }


    //other
    const other = () => {
        const G = G2.getEngine('canvas');
        const data = [
            {
                type: 'Quiz',
                value: syllabusDetail.quizScheme * 100,
            },
            {
                type: 'Assignment',
                value: syllabusDetail.assignmentScheme * 100,
            },
            {
                type: 'Final',
                value: syllabusDetail.finalScheme * 100,
            },
        ];

        const renderPieChart = () => {
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
        return (
            <div>
                <h1>Assignment Scheme</h1>
                {renderPieChart()}
            </div>
        )
    }
    const items = [
        {
            key: '1',
            label: `General`,
            children: generalChildren()
        },
        {
            key: '2',
            label: `OutLine`,
            children: test(),
        },
        {
            key: '3',
            label: `Others`,
            children: other(),
        },
    ];



    useEffect(() => {

        const fetchData = async () => {
            await getSyllabus(id);
            await getSyllabusDetail(id);
            // setSyllabusState(); // Kiểm tra xem bạn có cần gọi hàm này hay không
        };
        fetchData();
    }, [id, isModalVisible, isEvent, visibleModalMaterial, modalVisibleAddSection, modalDeleteUnit, modalVisibleAddLesson, modalDeleteLesson, modalVisibleAddMaterial, modalDeleteMaterial]);

    return (
        <Spin spinning={syllabusLoading} delay={0}>
            <Container sx={{ marginTop: "30px", minHeight: 360 }} fixed>
                <div style={headerStyle}>
                    <div>
                        <div style={syllabusNameStyle}>Syllabus</div>
                        <Title level={2}>{syllabusDetail?.name}</Title>
                        <div style={syllabusVersionStyle}>{syllabusDetail?.version}</div>
                    </div>

                    <div style={{ display: 'flex', alignItems: 'center' }}>
                        <div style={{ flex: '1' }} />
                        <div
                            style={{
                                ...statusIndicatorStyle,
                                backgroundColor: syllabusDetail?.active ? 'green' : 'gray',
                            }}
                        />
                        {syllabus?.isActive ? 'Active' : 'Inactive'}
                        <Dropdown overlay={menu} placement="bottomRight">
                            <Button type="link" icon={<MoreOutlined />} />
                        </Dropdown>
                    </div>
                </div>
                <Tabs
                    defaultActiveKey="1"
                    type="card"
                    size={"large"}
                    items={items}
                    style={{ marginBottom: '100px' }}
                />
            </Container>
        </Spin >
    )
}

export default ViewSyllabusDetail;