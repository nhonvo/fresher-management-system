import { Container } from '@mui/material'
import React, { useContext, useState, useEffect } from 'react'
import { TrainingProgramContext } from '../../context/TrainingProgramContext'
import Box from '@mui/material/Box';
import { Pagination } from '@mui/material';
import { Skeleton, Spin, Tag, Table, Space } from 'antd';
import { useNavigate } from "react-router-dom";


const columns = [
    {
        title: 'Name Training Program',
        dataIndex: 'name',
        key: 'name',
        render: (text) => <a>{text}</a>,
    },
    {
        title: 'Status',
        key: 'status',
        dataIndex: 'status',
        render: (status) => (
            <>
                {status == "Active" ? (
                    <Tag color={"green"} key={status}>
                        {status}
                    </Tag>
                ) : (
                    <Tag color={"red"} key={status}>
                        {status}
                    </Tag>
                )}

            </>
        ),
    }
];






function TrainingProgramView() {
    const navigate = useNavigate();
    const {
        TrainingProgramState: { trainingProgram, trainingProgramList, trainingProgramLoading, pageIndex, pageSize, totalPagesCount },
        getTrainingProgramList,
        setStateTrainingProgram
    } = useContext(TrainingProgramContext)
    console.log(trainingProgramList);
    const [pageState, setPageState] = useState({
        page: 0,
        pageSize: 10,
    });
    console.log(pageIndex);

    const handleOnChangePage = (event, pageIndex) => {
        setPageState(pre => {
            console.log(pageIndex);
            return {
                ...pre, page: pageIndex - 1
            }
        })
        setStateTrainingProgram()
    }
    useEffect(() =>
        getTrainingProgramList(pageState.page, pageState.pageSize),
        [pageState]);


    return (
        <>
            <Container sx={{ marginTop: "30px" }} fixed>
                <Spin spinning={trainingProgramLoading} delay={0}>
                    <Table
                        pagination={false}
                        columns={columns}
                        dataSource={trainingProgramList}
                        onRow={(record) => ({
                            onClick: () => {
                                setStateTrainingProgram();
                                navigate(`/TrainingProgram/${record.id}`)
                            }
                        })}
                    />
                </Spin>
                <Box justifyContent={"center"} alignItems={"center"} display={"flex"} sx={{
                    marginTop: "20px",
                    marginBottom: "100px"
                }}>
                    <Pagination
                        count={totalPagesCount + 1}
                        onChange={handleOnChangePage}
                    />
                </Box>
            </Container>
        </>
    )
}

export default TrainingProgramView