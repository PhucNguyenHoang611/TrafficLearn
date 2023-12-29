/* eslint-disable react-hooks/exhaustive-deps */
import { useDispatch, useSelector } from 'react-redux';
import React, { useEffect, useState } from 'react';

import {
  TextField,
  Button,
  Container,
  Grid,
  MenuItem,
  Select,
  FormControl,
  InputLabel,
  Typography,
} from '@mui/material';
import { getUser, updateUser } from '../apis/api_function';

const PersonalInfo = () => {
  const dispatch = useDispatch();
  const auth = useSelector(state => state.auth);
  const [userInfo, setUserInfo] = useState(null);
  const [userInfoTemp, setUserInfoTemp] = useState(null);

  const getUserInfo = () => {
    try {
      getUser(auth.id)
        .then((res) => {
          const item = { ...res.data, UserBirthday: new Date(res.data.UserBirthday).toISOString().split('T')[0] }

          setUserInfo(item);
          setUserInfoTemp(item);
        });
    } catch (error) {
      console.log(error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUserInfo((prevInfo) => ({
      ...prevInfo,
      [name]: value,
    }));
  };

  const resetChange = () => {
    setUserInfo(userInfoTemp);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    try {
      updateUser(auth.id, userInfo, auth.token);
      
      dispatch({
        type: "NOTIFY",
        payload: { type: "success", message: "Chỉnh sửa thông tin thành công !" }
      });

      getUserInfo
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    document.title = "Thông tin cá nhân";
    if (!userInfo && !userInfoTemp && auth.id)
      getUserInfo();
  }, []);

  return (
    <Container className="p-20">
      {userInfo && (
        <>
          <Typography variant="h4" gutterBottom>
            Thông tin cá nhân
          </Typography>
          <form className="mt-8" onSubmit={handleSubmit}>
            <Grid container spacing={3}>
              <Grid item xs={12}>
                <TextField
                  disabled={true}
                  fullWidth
                  label="Địa chỉ email"
                  name="UserEmail"
                  type="email"
                  value={userInfo.UserEmail}
                  onChange={handleChange}
                  required
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="Họ"
                  name="UserLastName"
                  value={userInfo.UserLastName}
                  onChange={handleChange}
                  required
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="Tên"
                  name="UserFirstName"
                  value={userInfo.UserFirstName}
                  onChange={handleChange}
                  required
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Ngày sinh"
                  name="UserBirthday"
                  type="date"
                  InputLabelProps={{
                    shrink: true,
                  }}
                  value={userInfo.UserBirthday}
                  onChange={handleChange}
                  required
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <FormControl fullWidth>
                  <InputLabel>Giới tính</InputLabel>
                  <Select
                    label="Giới tính"
                    name="UserGender"
                    value={userInfo.UserGender}
                    onChange={handleChange}
                    required
                  >
                    <MenuItem value="Male">Nam</MenuItem>
                    <MenuItem value="Female">Nữ</MenuItem>
                    <MenuItem value="Other">Khác</MenuItem>
                  </Select>
                </FormControl>
              </Grid>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="Số điện thoại"
                  name="UserPhoneNumber"
                  value={userInfo.UserPhoneNumber}
                  onChange={handleChange}
                  required
                />
              </Grid>
              <Grid item xs={12} className="flex justify-end items-center">
                <Button type="button" variant="contained" color="secondary" onClick={resetChange}>
                  Hủy thay đổi
                </Button>
                <Button type="submit" variant="contained" color="primary" sx={{ ml: 2 }}>
                  Lưu
                </Button>
              </Grid>
            </Grid>
          </form>
        </>
      )}
    </Container>
  );
};

export default PersonalInfo;
