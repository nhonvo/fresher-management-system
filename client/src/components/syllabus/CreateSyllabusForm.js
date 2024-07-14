import React, { useContext, useState } from 'react';
import { Form, Input, InputNumber, Button } from 'antd';
import { SyllabusContext } from '../../context/SyllabusContext';
import { useNavigate } from 'react-router-dom';
const CreateSyllabusForm = () => {

    const navigate = useNavigate()
    const { createSyllabus } = useContext(SyllabusContext)

    const [form] = Form.useForm();
    const [units, setUnits] = useState([]);
    const onFinish = async (values) => {
        // Triggers when the form is submitted
        values.units = values.units || [];
        const response = await createSyllabus(values)
        console.log(values);
        if (response.status == 200) {
            const newSyllabusId = response.data.id; // Assuming the ID is returned as syllabusId
            navigate(`/ViewSyllabus/${newSyllabusId}`);
        }
        console.log(response);

    };


    const handleUnitChange = (index, event) => {
        const updatedUnits = [...units];
        updatedUnits[index] = event.target.value;
        setUnits(updatedUnits);
    };

    return (
        <Form form={form} onFinish={onFinish}>
            <Form.Item label="Name" name="name" rules={[{ required: true }]}>
                <Input />
            </Form.Item>

            <Form.Item label="Code" name="code" rules={[{ required: true }]}>
                <Input />
            </Form.Item>

            <Form.Item label="Attendee Number" name="attendeeNumber" rules={[{ required: true }]}>
                <InputNumber />
            </Form.Item>

            <Form.Item label="Course Objective" name="courseObjective" rules={[{ required: true }]}>
                <Input />
            </Form.Item>

            <Form.Item label="Syllabus Level" name="syllabusLevel" rules={[{ required: true }]}>
                <Input />
            </Form.Item>

            <Form.Item label="Quiz Scheme" name="quizScheme" rules={[{ required: true }]}>
                <InputNumber />
            </Form.Item>

            <Form.Item label="Assignment Scheme" name="assignmentScheme" rules={[{ required: true }]}>
                <InputNumber />
            </Form.Item>

            <Form.Item label="Final Scheme" name="finalScheme" rules={[{ required: true }]}>
                <InputNumber />
            </Form.Item>

            <Form.Item label="Final Theory Scheme" name="finalTheoryScheme" rules={[{ required: true }]}>
                <InputNumber />
            </Form.Item>

            <Form.Item label="Final Practice Scheme" name="finalPracticeScheme" rules={[{ required: true }]}>
                <InputNumber />
            </Form.Item>

            <Form.Item label="GPA Scheme" name="gpaScheme" rules={[{ required: true }]}>
                <InputNumber />
            </Form.Item>

            <Form.List name="units">
                {(fields, { add, remove }) => (
                    <>
                        {fields.map((field, index) => (
                            <Form.Item
                                label={`Unit ${index + 1}`}
                                required={false}
                                key={field.key}
                            >
                                <Input
                                    value={units[index]}
                                    onChange={(event) => handleUnitChange(index, event)}
                                />
                            </Form.Item>
                        ))}
                        <Form.Item>
                            <Button type="dashed" onClick={() => add()} block>
                                Add Unit
                            </Button>
                        </Form.Item>
                    </>
                )}
            </Form.List>

            <Form.Item style={{ marginBottom: '100px' }}>
                <Button type="primary" htmlType="submit" >
                    Submit
                </Button>
            </Form.Item>
        </Form>
    );
};

export default CreateSyllabusForm;