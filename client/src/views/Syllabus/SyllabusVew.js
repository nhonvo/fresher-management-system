import React from 'react'
import Container from '@mui/material/Container';
import { useContext, useEffect, useState } from 'react';
import { SyllabusContext } from '../../context/SyllabusContext';
import { TrainingClassContext } from '../../context/TrainingClassContext';
import Box from '@mui/material/Box';
import { Pagination } from '@mui/material';
import { Skeleton, Spin, Button } from 'antd';
import { Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import { Space, Table, Tag, Modal } from 'antd';
import { apiUrl } from '../../context/Constants';
import axios from 'axios';

const columns = [
    {
        title: 'Name Syllabus',
        dataIndex: 'name',
        key: 'name',
        render: (text) => <a>{text}</a>,
    },
    {
        title: 'Level',
        dataIndex: 'syllabusLevel',
        key: 'level',
    },
    {
        title: 'AttendeeNumber',
        dataIndex: 'attendeeNumber',
        key: 'attendeeNumber',
    },
    {
        title: 'Code',
        key: 'code',
        dataIndex: 'code',
        render: (code) => (
            <>
                <Tag color={"green"} key={code}>
                    {code.toUpperCase()}
                </Tag>
            </>
        ),
    }
];

function SyllabusVew() {

    const navigate = useNavigate();
    const {
        syllabusState: { syllabus, syllabuses, syllabusLoading, pageIndex, pageSize, totalPagesCount, totalItemsCount },
        getSyllabuses,
        setSyllabusState,
        importFileSyllabus

    } = useContext(SyllabusContext);
    const [visibleModalMaterial, setVisibleModalMaterial] = useState(false)
    const [file, setFile] = useState(null)

    const [pageState, setPageState] = useState({
        page: 0,
        pageSize: 10,
    });
    console.log(pageIndex);

    const handleOnChangePage = (event, pageIndex) => {
        setSyllabusState()
        setPageState(pre => {
            return {
                ...pre, page: pageIndex - 1
            }
        })

    }
    const {
        getExcelFile } = useContext(TrainingClassContext);

    useEffect(() =>
        getSyllabuses(pageState.page, pageState.pageSize),
        [pageState]);

    const handleClickOpenDownload = () => {
        axios({
            method: 'GET',
            url: 'https://trainingmanagementsystem.azurewebsites.net/api/Syllabuses/export-syllabuses-csv',
            responseType: 'blob', // Kiểu dữ liệu phản hồi là file blob
        })
            .then(response => {
                // Xử lý phản hồi từ API
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', 'Syllabus.csv'); // Thiết lập tên file tải xuống
                document.body.appendChild(link);
                link.click();
            })
            .catch(error => {
                console.log('Lỗi khi tải xuống file:', error);
            });
    };

    // const url = `${apiUrl}Syllabuses/export-syllabuses-csv`;
    // window.open(url, '_blank'); // Mở liên kết trong cửa sổ/tab mới
    const handleClickOpenUpload = () => {

    }
    const handleUpload=async ()=>{
        let fileUpload = file
        const formData = new FormData();
        formData.append('FormFile', fileUpload)
        importFileSyllabus(formData)
        // await AddSyllabusMaterial(formData, editingSaveLessonMaterial.id)
        setVisibleModalMaterial(!visibleModalMaterial)
    }

    const handleMaterialAddCancel = () => {
        setVisibleModalMaterial(false);
    };

    const handleFileChange = (e) => {
        let file = e.target.files[0];
        setFile(file);

    };

    return (
        <Container sx={{ marginTop: "30px" }} fixed>
            <div style={{ display: "flex", justifyContent: "flex-end" }}>
                <Button
                    type="primary"
                    style={{ marginRight: "10px" }}
                    onClick={() => handleClickOpenDownload()}
                >
                    Export
                </Button>

                <Button
                    type="primary"
                    onClick={() => setVisibleModalMaterial(!visibleModalMaterial)}
                >
                    Import
                </Button>
            </div>

            <Spin spinning={syllabusLoading} delay={0}>
                <Table
                    pagination={false}
                    columns={columns}
                    dataSource={syllabuses}
                    onRow={(record) => ({
                        onClick: () => {
                            setSyllabusState();
                            navigate(`/ViewSyllabus/${record.id}`)
                        }
                    })}
                />
            </Spin>
            <Box justifyContent={"center"} alignItems={"center"} display={"flex"} sx={{
                marginTop: "20px",
                marginBottom: "20px"
            }}>
                <Pagination
                    count={totalPagesCount + 1}
                    onChange={handleOnChangePage}
                />
            </Box>

            <Modal
                title="Tải lên tập tin"
                visible={visibleModalMaterial}
                onOk={handleUpload}
                onCancel={handleMaterialAddCancel}
            >
                <input type='file' name='file' onChange={(e) => { handleFileChange(e) }} />

            </Modal>
        </Container>
    )
}


export default SyllabusVew




